using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：通往区块链大陆的路（撤离点）
 * 作者：sine5RAD
 */
public class EnterBlockChainLandTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UIManager.Instance.Push(new DialogueBoxPanel(DialogueBoxPanel.uIType, "来自区块链大陆矿业管理局的提示", "前面就是区块链大陆了，真的要继续前进吗？", EnterBlockChainLand));
        }
    }
    private void EnterBlockChainLand()
    {
        Debug.Log("非常抱歉，区块链大陆还在施工，前面的区域以后再来探索吧");
    }
}
