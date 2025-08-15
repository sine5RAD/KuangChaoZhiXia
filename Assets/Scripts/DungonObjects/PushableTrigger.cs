using System.Collections;
using System.Collections.Generic;
using KCGame;
using Sirenix.OdinInspector;
using UnityEngine;

/* 
 * 描述：可推动物体上挂载这个脚本
 * 作者：sine5RAD
 */
public class PushableTrigger : MonoBehaviour, IMapRole
{
    // 允许推动的标签
    public HashSet<string> allowPushTag = new HashSet<string>() { KCConstant.Tag_Player };

    [ShowInInspector]
    public Vector2Int CellPos { get; set; }
    public MapItemType RoleType { get; set; } = MapItemType.箱子;

    public BagLocalItemBase BagLocalItemBase { get; set ; } // 添加背包物品属性

    private void Start()
    {
        GameMapUnit.Instance.Register(this);
        CellPos = GameMapUnit.Instance.Fix_WorldToCell(transform.position);
    }

    public MapRoleProp MapRegister()
    {
        return new MapRoleProp(transform.position, MapItemType.箱子);
    }

    public void MoveTo(Vector2Int newCellPos)
    {
        CellPos = newCellPos;
        Vector3 targetPos = GameMapUnit.Instance.Fix_CellToWrold(newCellPos);
        StartCoroutine(AnimateMove(targetPos));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!allowPushTag.Contains(collision.gameObject.tag))
            return;

        // 获取碰撞方向
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        collision.GetContacts(contacts);
        Vector3Int direction = GetPushDirection(contacts[0].normal);

        // 转换为MoveDir枚举
        MoveDir moveDir = GetMoveDir(direction);

        // 尝试推动箱子
        GameMapUnit.Instance.TryMove(this, moveDir, out _);
    }

    private Vector3Int GetPushDirection(Vector2 normal)
    {
        if (normal.y == -1) return Vector3Int.down;    // 从上方碰撞
        if (normal.y == 1) return Vector3Int.up;       // 从下方碰撞
        if (normal.x == 1) return Vector3Int.right;    // 左边碰撞
        return Vector3Int.left;                       // 右边碰撞
    }

    private MoveDir GetMoveDir(Vector3Int dir)
    {
        if (dir.x > 0) return MoveDir.右;
        if (dir.x < 0) return MoveDir.左;
        if (dir.y > 0) return MoveDir.上;
        return MoveDir.下;
    }

    /// <summary>
    /// 如果需要移动动画，可以调用此方法
    /// </summary>
    public IEnumerator AnimateMove(Vector3 targetPos)
    {
        float duration = 0.2f;
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }
}