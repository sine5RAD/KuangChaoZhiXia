using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：比特币脚本
 * 作者：sine5RAD
 */
public class BitCoinTrigger : MonoBehaviour
{
    public PushableTrigger pushableTrigger;
    private GameObject _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞体是否是玩家
        if (collision.CompareTag("Player"))
        {
            _player = collision.gameObject;
            collision.gameObject.GetComponent<PlayerController>().SwitchInteractItem(OnPressE);
        }
        // 检查碰撞体是否是墙壁
        if (collision.CompareTag("Wall"))
        {
            pushableTrigger.StopMoving();
            _player.GetComponent<PlayerController>().StopMoving();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().RemoveInteractItem();
        }
    }
    private void OnPressE()
    {
        PlayerUIPanelController.Instance.player.AddItem(new BitCoin());
        Destroy(transform.parent.gameObject);
    }
}
