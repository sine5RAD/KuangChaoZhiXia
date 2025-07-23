using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：治疗胶囊（床）的触发器，TODO: 玩家交互后回满算力，算力池随真实时间恢复
 * 作者：sine5RAD
 */
public class CureCapsuleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().SwitchInteractItem(OnPressE);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().RemoveInteractItem();
        }
    }
    void OnPressE()
    {
        Debug.Log("遇到困难，睡大觉");
    }
}
