using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：货币物块碰撞箱基类
 * 作者：sine5RAD
 */
public class CoinTriggerBase : MonoBehaviour
{
    public CoinBase coinInfo;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞体是否是玩家
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().SwitchInteractItem(OnPressE);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        // 检查碰撞体是否是玩家
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().RemoveInteractItem();
        }
    }

    protected virtual void OnPressE()
    {
        GameMapUnit.Instance.UnRegister(transform.parent.GetComponent<PushableTrigger>());
        PlayerUIPanelController.Instance.player.AddItem(coinInfo);
        Destroy(transform.parent.gameObject);
    }

    public virtual void Init()
    {
        Debug.Log("CoinTriggerBase Init");
    }
}
