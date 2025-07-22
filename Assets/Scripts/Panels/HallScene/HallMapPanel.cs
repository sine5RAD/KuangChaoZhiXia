using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：大厅地图界面
 * 作者：sine5RAD
 */
public class HallMapPanel : BasePanel
{
    private static readonly string _name = "HallMapPanel";
    private static readonly string _path = "Prefab/Panel/HallScene/HallMapPanel";
    public static readonly UIType uIType = new UIType(_name, _path);
    public HallMapPanel(UIType uIType) : base(uIType)
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
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
