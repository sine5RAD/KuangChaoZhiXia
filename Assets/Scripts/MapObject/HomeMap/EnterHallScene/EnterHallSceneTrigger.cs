using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：
 * 作者：sine5RAD
 */
public class EnterHallSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UIManager.Instance.Push(new DialogueBoxPanel(DialogueBoxPanel.uIType, "", "离开矿工之家吗？", EnterHallScene));
        }
    }
    private void EnterHallScene()
    {
        KCGame.SceneController.Instance.LoadScene("HallScene", new HallScene());
    }
}
