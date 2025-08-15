using KCGame;
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
        _player.GPU.Temperature += KCConstant.RushTempDelta;
        Update();
        return true;
    }

    public void Move()
    {
        _player.GPU.Temperature += KCConstant.MoveTempDelta;
        Update();
    }

    /// <summary>
    /// 获取物体实际重量
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private float GetRealWeight(BagLocalItemBase item)
    {
        return item.Weight * (1 + _player.GPU.TC / 10);
    }

    public void Push(IMapRole mapRole)
    {
        Debug.Log(KCConstant.PushTempDelta + GetRealWeight(mapRole.BagLocalItemBase) * KCConstant.PushWeightFactor);
        _player.GPU.Temperature += KCConstant.PushTempDelta + GetRealWeight(mapRole.BagLocalItemBase) * KCConstant.PushWeightFactor;
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
