using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * 描述：玩家数据类，包含燃料，燃料最大值，算力，背包
 * 作者：sine5RAD
 */
public class Player: KCGame.KCSingleton<Player>
{
    private float _gasVal;//燃料用float存储，但显示的时候四舍五入取整
    public float GasVal 
    {
        get { return _gasVal; }
        set
        {
            _gasVal = value;
            if(_gasVal < 0)_gasVal = 0;
            if(_gasVal > GasLimit)_gasVal = GasLimit;
        }
    }
    private readonly float _basicGasLimit;//基础燃料最大值
    public float GasLimit { get { return _basicGasLimit; }}
    private readonly int _basicCalcPower;//空手算力
    public int CalcPower {  get { return _basicCalcPower; } }

    private readonly float _basicRushCoolDown;//基础冲刺冷却
    public float RushCoolDown
    {
        get
        {
            return _basicRushCoolDown;
        }
    }

    private float _playerRushCoolDown;//当前玩家冲刺冷却
    public float PlayerRushCoolDown
    {
        get => _playerRushCoolDown;
        set
        {
            _playerRushCoolDown = value;
            if (_playerRushCoolDown <= 0) _playerRushCoolDown = 0;
        }
    }
    /// <summary>
    /// 依据所在场景的不同返回不同的耗散速率
    /// </summary>
    public static float GasDissipationRate
    {
        get
        {
            switch(SceneManager.GetActiveScene().name)
            {
                case "HallScene":
                case "HomeScene":
                    return 0;
                default:
                    return 1;
            }
        }
    }

    private BagLocalData _bag = new BagLocalData();
    public BagLocalData Bag
    {
        get { return _bag; }
    }
    /// <summary>
    /// 向背包中添加物品
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(BagLocalItemBase item)
    {
        _bag.items.Add(item);
    }
    /// <summary>
    /// 从背包中移除物品
    /// </summary>
    /// <param name="index"></param>
    public void RemoveItem(int index)
    {
        if (index < 0 || index >= _bag.items.Count)
        {
            Debug.LogError("Index out of range when removing item from bag.");
            return;
        }
        _bag.items.RemoveAt(index);
    }
    public Player()
    {
        _basicGasLimit = 1000;
        _basicCalcPower = 50;
        _gasVal = 1000;
        _basicRushCoolDown = 5;
        _playerRushCoolDown = 0;
    }
}
