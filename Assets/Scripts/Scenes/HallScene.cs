using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * 描述：大厅场景
 * 作者：sine5RAD
 */
public class HallScene : KCGame.SceneBase
{
    public override void EnterScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        base.EnterScene(scene, loadSceneMode);
        UIManager.Instance.Push(new PlayerUIPanel(PlayerUIPanel.uIType));
        Debug.Log("Rua!");
    }

    public override void ExitScene()
    {
    }
}
