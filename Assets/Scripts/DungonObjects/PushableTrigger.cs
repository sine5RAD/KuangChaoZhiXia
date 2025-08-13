using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：可推动物体上挂载这个脚本
 * 作者：sine5RAD
 */
public class PushableTrigger : MoveableObject
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        collision.GetContacts(contacts);
        Vector3Int pos = _mapGrid.WorldToCell(transform.position);
        Vector3Int direction = new Vector3Int(0, 0, 0);
        if (contacts[0].normal.y == -1)//从上方碰撞
        {
            direction = Vector3Int.down;
        }
        else if (contacts[0].normal.y == 1)//从下方碰撞
        {
            direction = Vector3Int.up;
        }
        else if (contacts[0].normal.x == 1)//左边碰撞
        {
            direction = Vector3Int.right;
        }
        else if (contacts[0].normal.x == -1)//右边碰撞
        {
            direction = Vector3Int.left;
        }
        _moveCoroutine = StartCoroutine(Move(transform.position, _mapGrid.CellToWorld(pos + direction) + new Vector3(0.64f, 0.64f, 0), PlayerUIPanelController.Instance.player.CurrentMovingCooldown));
    }

}
