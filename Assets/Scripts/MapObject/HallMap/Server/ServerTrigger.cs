using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：可交互物体矿材（server）的触发器，玩家交互后打开种植（？）面板
 * 作者：sine5RAD
 */
public class ServerTrigger : MonoBehaviour
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
        Debug.Log("开启种植页面");
        UIManager.Instance.Push(new ServerControllerPanel(ServerControllerPanel.uIType));
    }
}
