using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * 描述：房间场景
 * 作者：sine5RAD
 */
public class HomeScene : SceneBase
{
    public override void EnterScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        base.EnterScene(scene, loadSceneMode);
        UIManager.Instance.Push(new PlayerUIPanel(PlayerUIPanel.uIType));
    }

    public override void ExitScene()
    {
    }
}
