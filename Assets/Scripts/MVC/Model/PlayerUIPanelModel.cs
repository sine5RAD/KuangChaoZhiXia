using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* 
 * 描述：玩家信息UI的数据类
 * 作者：sine5RAD
 */
public class PlayerUIPanelModel
{
    Player _player;
    public Player Player { get { return _player; } }
    public PlayerUIPanelModel(Player player)
    {
        _player = player;
    }

    public void UpdatePerFrame()
    {
        _player.UpdatePerFrame();
        Update();
    }

    public bool InvokeRushSkill()
    {
        if (_player.PlayerRushCoolDown > 0) return false;
        _player.PlayerRushCoolDown += _player.RushCoolDown;
        Update();
        return true;
    }

    private event UnityAction<PlayerUIPanelModel> _onUpdate;
    public void AddListener(UnityAction<PlayerUIPanelModel> onUpdate)
    {
        _onUpdate += onUpdate;
    }
    public void RemoveListener(UnityAction<PlayerUIPanelModel> onUpdate)
    {
        _onUpdate -= onUpdate;
    }

    private void Update()
    {
        _onUpdate?.Invoke(this);
    }
}
