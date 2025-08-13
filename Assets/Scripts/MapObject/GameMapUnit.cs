using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using KCGame;

public enum MoveDir
{
    上,
    下,
    左,
    右,
}

public struct MapRoleProp
{
    public MapItemType mapItemType;
    public Vector3 pos;

    public MapRoleProp(Vector3 pos, MapItemType type)
    {
        this.pos = pos;
        this.mapItemType = type;
    }
}

public enum MapItemType
{
    空,
    玩家,
    障碍物,
    箱子
}

/// <summary>
/// 地图角色接口
/// </summary>
public interface IMapRole
{
    Vector2Int CellPos { get; set; }
    MapItemType RoleType { get; set; }
    MapRoleProp MapRegister();
    void MoveTo(Vector2Int newCellPos); // 新增方法用于移动角色
}

/// <summary>
/// 地图管理单元
/// </summary>
public class GameMapUnit : MonoBehaviour
{
    public Grid MapGrid;
    public Tilemap groundMap;
    public Tilemap buildMap;

    // 存储静态障碍物信息
    private Dictionary<Vector2Int, bool> dic_StaticObstacle;

    // 存储动态物体信息
    private Dictionary<Vector2Int, IMapRole> dic_CellToRole;

    // 单例实例
    private static GameMapUnit _instance;
    public static GameMapUnit Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameMapUnit>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameMapUnit (Singleton)");
                    _instance = go.AddComponent<GameMapUnit>();
                    Debug.LogWarning("GameMapUnit instance created automatically in scene");
                }
            }

        
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        _instance.InitMapData();
    }


    private List<TileBase> ObsTile;

    [Button]
    public void InitMapData()
    {
        MapGrid = GameObject.FindWithTag(KCConstant.Tag_Map)?.GetComponent<Grid>();
        groundMap = GameObject.FindWithTag(KCConstant.Tag_MapGround)?.GetComponent<Tilemap>();
        buildMap = GameObject.FindWithTag(KCConstant.Tag_MapObstacle)?.GetComponent<Tilemap>();

        if (groundMap == null || MapGrid == null)
        {
            Debug.LogWarning("没有找到MapGrid 或 groundMap");
            return;
        }
        ObsTile = GameRoot.Instance.KCAssets.ObsTiles;


        // 初始化字典
        dic_StaticObstacle = new Dictionary<Vector2Int, bool>();
        dic_CellToRole = new Dictionary<Vector2Int, IMapRole>();

        // 获取groundMap中所有有效瓦片位置
        foreach (Vector3Int cellPosition in groundMap.cellBounds.allPositionsWithin)
        {
            if (!groundMap.HasTile(cellPosition)) continue;

            Vector2Int pos2D = new Vector2Int(cellPosition.x, cellPosition.y);

            // 标记静态障碍物：buildMap有瓦片且该瓦片存在于ObsTile列表中
            bool isObstacle = false;
            if (buildMap != null && ObsTile != null)
            {
                // 获取当前位置的瓦片
                TileBase currentTile = buildMap.GetTile(cellPosition);
                // 检查瓦片是否存在且在ObsTile列表中
                isObstacle = currentTile != null && ObsTile.Contains(currentTile);
            }

            dic_StaticObstacle[pos2D] = isObstacle;
            // 初始化动态物体字典
            dic_CellToRole[pos2D] = null;
        }

        Debug.Log($"Map data initialized. {dic_StaticObstacle.Count} cells processed.");
    }

    /// <summary>
    /// 检查位置是否可通行
    /// </summary>
    public bool IsPassable(Vector2Int position)
    {


        // 检查位置是否存在
        if (!dic_StaticObstacle.ContainsKey(position))
        {
            return false;
        }

        // 检查静态障碍物
        if (dic_StaticObstacle[position])
        {
            return false;
        }

        // 检查动态物体
        if (dic_CellToRole.TryGetValue(position, out IMapRole role) && role != null)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 世界坐标转格子坐标
    /// </summary>
    public Vector2Int Fix_WorldToCell(Vector3 W_Pos)
    {


        Vector3Int pos = MapGrid.WorldToCell(W_Pos);
        return new Vector2Int(pos.x, pos.y);
    }

    /// <summary>
    /// 格子坐标转世界坐标
    /// </summary>
    public Vector3 Fix_CellToWrold(Vector2Int vector2Int)
    {


        return Fix_CellToWrold(new Vector3Int(vector2Int.x, vector2Int.y, 0));
    }

    public Vector3 Fix_CellToWrold(Vector3Int C_Pos)
    {


        Vector3 pos = MapGrid.CellToWorld(C_Pos);
        return new Vector3(pos.x + 0.64f, pos.y + 0.64f, pos.z);
    }

    /// <summary>
    /// 注册地图角色
    /// </summary>
    public void Register(IMapRole r)
    {



        MapRoleProp p = r.MapRegister();
        Vector2Int cellPos = Fix_WorldToCell(p.pos);
        r.CellPos = cellPos;

        // 检查位置是否已被占用
        if (dic_CellToRole.ContainsKey(cellPos) && dic_CellToRole[cellPos] != null)
        {
            Debug.LogWarning($"位置 {cellPos} 已被占用，无法注册新角色");
            return;
        }

        dic_CellToRole[cellPos] = r;
    }

    /// <summary>
    /// 尝试移动角色
    /// </summary>
    public bool TryMove(IMapRole role, MoveDir dir, out Vector3 worldPos)
    {
        worldPos = Vector3.zero;
        Vector2Int targetPos = GetTargetCellPos(role.CellPos, dir);

        // 检查目标位置是否存在
        if (!dic_StaticObstacle.ContainsKey(targetPos))
        {
            Debug.Log($"目标位置 {targetPos} 不存在");
            return false;
        }

        // 检查目标位置是否有静态障碍物
        if (dic_StaticObstacle[targetPos])
        {
            Debug.Log($"目标位置 {targetPos} 有静态障碍物");
            return false;
        }

        // 获取目标位置的角色
        IMapRole targetRole = dic_CellToRole.ContainsKey(targetPos) ? dic_CellToRole[targetPos] : null;

        // 目标位置为空，直接移动
        if (targetRole == null)
        {
            return MoveRole(role, targetPos, out worldPos);
        }

        // 目标位置是箱子，尝试推动
        if (targetRole.RoleType == MapItemType.箱子)
        {
            Vector2Int boxTargetPos = GetTargetCellPos(targetPos, dir);

            // 检查箱子目标位置是否可通行
            if (IsPassable(boxTargetPos))
            {
                // 移动箱子
                if (!MoveRole(targetRole, boxTargetPos, out _))
                {
                    return false;
                }

                // 移动玩家
                return MoveRole(role, targetPos, out worldPos);
            }
        }

        Debug.Log($"目标位置 {targetPos} 被 {targetRole.RoleType} 阻挡");
        return false;
    }

    /// <summary>
    /// 移动角色到新位置
    /// </summary>
    [Button]
    private bool MoveRole(IMapRole role, Vector2Int newPos, out Vector3 worldPos)
    {
        worldPos = Vector3.zero;

        // 更新角色位置
        Vector2Int oldPos = role.CellPos;

        // 更新字典
        dic_CellToRole[oldPos] = null;
        dic_CellToRole[newPos] = role;

        // 调用角色的移动方法
        role.MoveTo(newPos);

        // 计算世界坐标
        worldPos = Fix_CellToWrold(newPos);
        return true;
    }

    /// <summary>
    /// 获取目标格子位置
    /// </summary>
    public Vector2Int GetTargetCellPos(Vector2Int cellPos, MoveDir dir)
    {
        switch (dir)
        {
            case MoveDir.上: return new Vector2Int(cellPos.x, cellPos.y + 1);
            case MoveDir.下: return new Vector2Int(cellPos.x, cellPos.y - 1);
            case MoveDir.左: return new Vector2Int(cellPos.x - 1, cellPos.y);
            case MoveDir.右: return new Vector2Int(cellPos.x + 1, cellPos.y);
            default: return cellPos;
        }
    }

    #region Debug
    public GameObject testObj;

    [Button]
    public void ShowCell(Vector3Int v)
    {
        Instantiate(testObj, MapGrid.CellToWorld(v), Quaternion.identity);
    }



    #endregion
}