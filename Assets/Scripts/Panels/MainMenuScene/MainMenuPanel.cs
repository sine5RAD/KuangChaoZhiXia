using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
 * 描述：主菜单UI
 * 作者：sine5RAD
 */
public class MainMenuPanel : KCGame.BasePanel
{
    private static readonly string _name = "MainMenuPanel";
    private static readonly string _path = "Prefab/Panel/MainMenuScene/MainMenuPanel";
    public static readonly UIType uIType = new UIType(_name, _path);

    Button _startGameBtn;
    public MainMenuPanel(UIType uIType) : base(uIType)
    {
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnStart()
    {
        base.OnStart();
        _startGameBtn = UIMethods.Instance.GetComponentInChildren<Button>(activeObj, "StartGame");
        _startGameBtn.onClick.AddListener(StartGame);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    private void StartGame()
    {
        SceneController.Instance.LoadScene("HallScene", new HallScene());
    }
}
