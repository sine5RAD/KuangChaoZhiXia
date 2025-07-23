using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：矿材终端UI
 * 作者：sine5RAD
 */
public class ServerControllerPanel : BasePanel
{
    private static readonly string _name = "ServerControllerPanel";
    private static readonly string _path = "Prefab/Panel/HallScene/ServerControllerPanel";
    public static readonly UIType uIType = new UIType(_name, _path);
    public ServerControllerPanel(UIType uIType) : base(uIType)
    {
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        InputUtility.Instance.UnlockMoving();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        InputUtility.Instance.LockMoving();
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            UIManager.Instance.Pop();
        }
    }
}
