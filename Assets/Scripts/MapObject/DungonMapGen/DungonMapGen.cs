using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

/* 
 * 描述：生成地牢
 * 作者：sine5RAD
 */
public class DungonMapGen : MonoBehaviour
{
    private Tilemap _groundTileMap;
    public int seed;
    public bool useRandomSeed;

    #region MapObjectManagerSetting

    private Grid _mapGrid;
    private Tilemap _buildingTileMap;
    private GameObject _buildingObject;
    public GameObject player;
    #endregion

    [Range(0, 1f)]
    public float groundProbability;  //生成地板的频率

    [Range(0, 0.1f)]
    public float lancunarity;              //缩放
    public TileBase groundTile;            //地板的瓷砖
    public TileBase wallTile;              //墙面的瓷砖
    public TileBase playerSpawnerTile;     //重生点瓷砖
    public TileBase exitTile;              //出口瓷砖
    public int width, height;              //地图大小

    private bool[,] _groundData;//True: 地板 Falue: 空
    private BuildingType[, ] _buildingData;
    private enum GroundTileType
    {
        None,
        Ground
    }

    private enum BuildingType
    {
        Null,
        Wall,
        PlayerSpawner
    }
    // Start is called before the first frame update
    void Start()
    {
        _groundTileMap = transform.Find("DungonGround").GetComponent<Tilemap>();
        _mapGrid = transform.GetComponent<Grid>();
        _buildingTileMap = transform.Find("DungonBuilding").GetComponent<Tilemap>();
        _buildingObject = transform.Find("BuildingObject").gameObject;
    }

    public void GenerateMap()
    {
        GenerateMapData();
        GenerateTileMap();

        #region MapObjectManager设置
        var mapObjectManager = transform.AddComponent<MapObjectManager>();
        mapObjectManager.mapGrid = _mapGrid;
        mapObjectManager.tilemap = _buildingTileMap;
        mapObjectManager.buildingObject = _buildingObject;
        mapObjectManager.player = player;
        #endregion
    }

    private void GenerateMapData()
    {
        if(useRandomSeed)
        {
            seed = Time.time.GetHashCode();
        }
        UnityEngine.Random.InitState(seed);
        float randomOffset = UnityEngine.Random.Range(-10000, 10000);

        _groundData = new bool[width, height];
        _buildingData = new BuildingType[width, height];

        #region 生成地板
        Debug.Log("生成地板");
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float noiseValue = Mathf.PerlinNoise(i * lancunarity + randomOffset, j * lancunarity + randomOffset);
                if(noiseValue < groundProbability)
                {
                    _groundData[i, j] = true;
                }
                else _groundData[i, j] = false;
            }
        }
        #endregion

        #region 生成墙体
        Debug.Log("生成墙体");
        GenerateWall();
        #endregion

        #region 清理小房间
        List<Vector2Int> largestRoom = FindLargestRoom();
        Array.Clear(_groundData, 0, _groundData.Length);
        foreach (Vector2Int pos in largestRoom)
            _groundData[pos.x, pos.y] = true;
        #endregion

        #region 重新生成墙体
        Debug.Log("重新生成墙体");
        GenerateWall();
        #endregion

        #region 将出生点置于左下角第一个不为空的地块
        int playerSpawnerPosSum = 2147483647;
        Vector2Int playerSpawnerPos = new Vector2Int();
        for(int i = 0;i < largestRoom.Count;i++)
        {
            var p = largestRoom[i];
            if (IsAreaEdge(p.x, p.y)) continue;
            if(p.x + p.y < playerSpawnerPosSum)
            {
                playerSpawnerPosSum = p.x + p.y;
                playerSpawnerPos = p;
            }
        }
        _buildingData[playerSpawnerPos.x, playerSpawnerPos.y] = BuildingType.PlayerSpawner;

        #endregion
    }

    /// <summary>
    /// 找到地图中最大的联通房间
    /// </summary>
    /// <param name="largestRoom"></param>
    private List<Vector2Int> FindLargestRoom()
    {
        List<Vector2Int> largestRoom = new List<Vector2Int>();
        List<Vector2Int> currentRoom = new List<Vector2Int>();
        bool[,] vis = new bool[width, height];
        Queue<Vector2Int> bfsQueue = new Queue<Vector2Int>();
        Vector2Int[] move = { new Vector2Int(-1, 0), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(0, 1) };
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (vis[i, j]) continue;
                if (_groundData[i, j] == false)
                {
                    vis[i, j] = true;
                    continue;
                }
                if (_buildingData[i, j] == BuildingType.Wall) continue;
                bfsQueue.Enqueue(new Vector2Int(i, j));
                while(bfsQueue.Count > 0)
                {
                    int x = bfsQueue.Peek().x, y = bfsQueue.Peek().y;
                    bfsQueue.Dequeue();
                    if (x < 0 || x >= width || y < 0 || y >= height) continue;  //边界检查
                    if (vis[x, y]) continue;                                    //不检查重复格子
                    if (_groundData[x, y] == false) continue;                   //不添加空格子
                    vis[x, y] = true;
                    currentRoom.Add(new Vector2Int(x, y));
                    if (_buildingData[x, y] == BuildingType.Wall) continue;
                    for(int k = 0;k < 4;k++)
                    {
                        bfsQueue.Enqueue(new Vector2Int(x + move[k].x, y + move[k].y));
                    }
                }
                if(currentRoom.Count > largestRoom.Count)
                    largestRoom = new List<Vector2Int>(currentRoom.ToArray());
                currentRoom.Clear();
            }
        }
        Debug.Log(largestRoom.Count);
        return largestRoom;
    }

    /// <summary>
    /// 判断该坐标是否为墙体
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private bool IsAreaEdge(int x, int y)
    {
        if(_groundData[x, y] == false)return false;
        if (x == 0 || y == 0 || x == width - 1 || y == height - 1) return true;
        return !(_groundData[x - 1, y] && _groundData[x + 1, y] && _groundData[x, y - 1] && _groundData[x, y + 1]);
    }

    /// <summary>
    /// 生成墙体
    /// </summary>
    private void GenerateWall()
    {
        Array.Clear(_buildingData, 0, _buildingData.Length);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (IsAreaEdge(i, j))
                {
                    _buildingData[i, j] = BuildingType.Wall;
                }
            }
        }
    }
    public void GenerateTileMap()
    {
        CleanTileMap();
        TileBase tile;
        #region 填充地砖
        for (int i = 0; i < width; i++)
        {
            for(int j = 0;j < height; j++)
            {
                tile = _groundData[i, j] ? groundTile : null;
                _groundTileMap.SetTile(new Vector3Int(i, j), tile);
            }
        }
        #endregion

        #region 填充地图建筑
        for (int i = 0;i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                switch (_buildingData[i, j])
                {
                    case BuildingType.Null: break;
                    case BuildingType.Wall:
                        tile = wallTile;
                        _buildingTileMap.SetTile(new Vector3Int(i, j), tile);
                        break;
                    case BuildingType.PlayerSpawner:
                        tile = playerSpawnerTile;
                        _buildingTileMap.SetTile(new Vector3Int(i, j), tile);
                        break;
                }
            }
        }
        #endregion
    }

    public void CleanTileMap()
    {
        _groundTileMap.ClearAllTiles();
        _buildingTileMap.ClearAllTiles();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
