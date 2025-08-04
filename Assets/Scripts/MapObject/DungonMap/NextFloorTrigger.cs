using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：进入下一层触发器
 * 作者：sine5RAD
 */
public class NextFloorTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UIManager.Instance.Push(new DialogueBoxPanel(DialogueBoxPanel.uIType, "来自区块链大陆矿业管理局的提示", $"下一层难度等级为{DungonConfig.Instance.difficulty + DungonConfig.Instance.difficultyGap}，真的要继续前进吗？", EnterNextFloor));
        }
    }
    private void EnterNextFloor()
    {
        DungonConfig.Instance.floor -= 1;
        DungonConfig.Instance.difficulty += DungonConfig.Instance.difficultyGap;
        SceneController.Instance.LoadScene(
            $"BlockChainLandScene", 
            new BlockChainLandScene(DungonConfig.Instance.floor, DungonConfig.Instance.difficulty, DungonConfig.Instance.difficultyGap),
            true
            );
    }
}
