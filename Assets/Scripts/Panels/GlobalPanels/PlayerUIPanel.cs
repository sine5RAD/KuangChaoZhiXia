using KCGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* 
 * 描述：玩家ui界面
 * 作者：sine5RAD
 */
public class PlayerUIPanel : BasePanel
{
    private static readonly string _name = "PlayerUIPanel";
    private static readonly string _path = "Prefab/Panel/GlobalPanels/PlayerUIPanel";
    public static readonly UIType uIType = new UIType(_name, _path);
    private Slider _gasVal, _calcPowerVal;
    private TextMeshProUGUI _gasProporty, _calcPowerProporty;
    public PlayerUIPanel(UIType uIType) : base(uIType)
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
        _gasVal = UIMethods.Instance.GetComponentInChildren<Slider>(activeObj, "GasVal");
        _calcPowerVal = UIMethods.Instance.GetComponentInChildren<Slider>(activeObj, "CalcPowerVal");
        _gasProporty = UIMethods.Instance.GetComponentInChildren<TextMeshProUGUI>(activeObj, "GasProporty");
        _calcPowerProporty = UIMethods.Instance.GetComponentInChildren<TextMeshProUGUI>(activeObj, "CalcPowerProporty");
    }

    public void UpdateInfo(PlayerUIPanelModel playerUIPanelModel)
    {
        _gasVal.maxValue = playerUIPanelModel.Player.GasLimit;
        _gasVal.value = playerUIPanelModel.Player.GasVal;
        _gasProporty.text = $"{(int)playerUIPanelModel.Player.GasVal}/{playerUIPanelModel.Player.GasLimit}";
        _calcPowerVal.value = playerUIPanelModel.Player.CalcPower;
        _calcPowerProporty.text = $"{(int)playerUIPanelModel.Player.CalcPower}/1000";
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
