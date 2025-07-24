using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：主界面UI的控制器
 * 作者：sine5RAD
 */
public class PlayerUIPanelController : MonoBehaviour
{
    private PlayerUIPanel _playerUIPanel;
    private PlayerUIPanelModel _playerUIPanelModel;
    // Start is called before the first frame update
    void Start()
    {
        if (UIManager.Instance.CurrentPanel is PlayerUIPanel)
            _playerUIPanel = UIManager.Instance.CurrentPanel as PlayerUIPanel;
        else Debug.LogError("当前UI不是PlayerUIPanel");
        _playerUIPanelModel = new PlayerUIPanelModel(new Player());
        _playerUIPanelModel.AddListener(_playerUIPanel.UpdateInfo);
    }

    // Update is called once per frame
    void Update()
    {
        _playerUIPanelModel.UpdatePerFrame();
    }
}
