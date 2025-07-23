using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：可交互物体home的触发器，玩家交互后进入房间界面
 * 作者：sine5RAD
 */
public class HomeTrigger : MonoBehaviour
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
        Debug.Log("进入房间");
    }
}
