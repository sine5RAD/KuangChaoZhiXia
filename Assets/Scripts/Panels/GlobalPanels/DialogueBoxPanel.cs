using KCGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/* 
 * 描述：对话框，点击确定/Y执行一个无参函数，点击取消/N或者按esc无事发生
 *       可以自定义标题与正文
 * TODO：文字自适应
 * 作者：sine5RAD
 */
public class DialogueBoxPanel : BasePanel
{
    private static readonly string _name = "DialogueBoxPanel";
    private static readonly string _path = "Prefab/Panel/GlobalPanels/DialogueBoxPanel";
    public static UIType uIType = new UIType(_name, _path);

    private TextMeshProUGUI _titleTMP, _textTMP;
    private string _title, _text;
    private Button _yes, _no;
    private event UnityAction onConfirm;
    public DialogueBoxPanel(UIType uIType, string title, string text, UnityAction func) : base(uIType)
    {
        _title = title;
        _text = text;
        onConfirm += func;
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
        _titleTMP = UIMethods.Instance.GetComponentInChildren<TextMeshProUGUI>(activeObj, "Title");
        _titleTMP.text = _title;
        _textTMP = UIMethods.Instance.GetComponentInChildren<TextMeshProUGUI>(activeObj, "Text");
        _textTMP.text = _text;
        _yes = UIMethods.Instance.GetComponentInChildren<Button>(activeObj, "Yes");
        _yes.onClick.AddListener(Confirm);
        _no = UIMethods.Instance.GetComponentInChildren<Button>(activeObj, "No");
        _no.onClick.AddListener(Refuse);
        InputUtility.Instance.LockMoving();
    }

    /// <summary>
    /// 确认，执行目标函数之后关闭窗口
    /// </summary>
    private void Confirm() 
    { 
        UIManager.Instance.Pop();
        InputUtility.Instance.UnlockMoving(); 
        onConfirm?.Invoke();
    }
    /// <summary>
    /// 取消，直接关闭窗口，不执行函数
    /// </summary>
    private void Refuse() 
    { 
        UIManager.Instance.Pop();
        InputUtility.Instance.UnlockMoving();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(Input.GetKeyDown(KeyCode.Y))Confirm();
        if(Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Escape))Refuse();
    }
}
