using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：比特币脚本
 * 作者：sine5RAD
 */
public class BitCoinTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().SwitchInteractItem(OnPressE);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().RemoveInteractItem();
        }
    }
    void OnPressE()
    {
        PlayerUIPanelController.Instance.player.AddItem(new BitCoin());
        Destroy(transform.parent.gameObject);
    }
}
