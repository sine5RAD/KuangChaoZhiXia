using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：主界面UI的控制器
 * 作者：sine5RAD
 */
public class PlayerUIPanelController : KCGame.KCSingletonMonoBehaviour<PlayerUIPanelController>
{
    private PlayerUIPanel _playerUIPanel;
    private PlayerUIPanelModel _playerUIPanelModel;

    public Player player
    {
        get { return _playerUIPanelModel.Player; }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (UIManager.Instance.CurrentPanel is PlayerUIPanel)
            _playerUIPanel = UIManager.Instance.CurrentPanel as PlayerUIPanel;
        else Debug.LogError("当前UI不是PlayerUIPanel");
        _playerUIPanelModel = new PlayerUIPanelModel(DungonConfig.Instance.player);
        _playerUIPanelModel.AddListener(_playerUIPanel.UpdateInfo);
    }

    // Update is called once per frame
    void Update()
    {
        _playerUIPanelModel.UpdatePerFrame();
    }

    public bool InvokeRushSkill()
    {
        return _playerUIPanelModel.InvokeRushSkill();
    }

    public void Move()
    {
        if (_playerUIPanel != null)
        {
            _playerUIPanelModel.Move();
        }
        else
        {
            Debug.LogError("PlayerUIPanel is null");
        }
    }

    public void Push(IMapRole mapRole)
    {
        if (_playerUIPanel != null)
        {
            _playerUIPanelModel.Push(mapRole);
        }
        else
        {
            Debug.LogError("PlayerUIPanel is null");
        }
    }
}
