using KCGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

/* 
 * 描述：局内背包界面
 * 作者：sine5RAD
 */
public class BagPanel : BasePanel
{
    private static readonly string _name = "BagPanel";
    private static readonly string _path = "Prefab/Panel/GlobalPanels/BagPanel";
    public static readonly UIType uIType = new UIType(_name, _path);

    private Button _exit;


    private TextMeshProUGUI _descriptionView;
    private Button _discard;
    public Player player;
    public BagPanel(UIType uIType, Player player) : base(uIType)
    {
        this.player = player;
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
        _exit = UIMethods.Instance.GetComponentInChildren<Button>(activeObj, "Exit");
        _exit.onClick.AddListener(Exit);
        _descriptionView = UIMethods.Instance.GetComponentInChildren<TextMeshProUGUI>(activeObj, "DescriptionView");
        _descriptionView.text = "";
    }

    public void Update(BagPanelModel bagPanelModel)
    {
        Debug.Log(BagInfoSearcher.Instance.GetInfoFromName(bagPanelModel.SelectedItem.Name).itemDescreption);
        _descriptionView.text = BagInfoSearcher.Instance.GetInfoFromName(bagPanelModel.SelectedItem.Name).itemDescreption;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            Exit();
        }
    }

    private void Exit()
    {
        UIManager.Instance.Pop();
    }
}
