using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * 描述：主菜单场景
 * 作者：sine5RAD
 */
public class MainMenuScene : KCGame.SceneBase
{
    public override void EnterScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        base.EnterScene(scene, loadSceneMode);
        UIManager.Instance.Push(new MainMenuPanel(MainMenuPanel.uIType));
    }

    public override void ExitScene()
    {
    }
}
