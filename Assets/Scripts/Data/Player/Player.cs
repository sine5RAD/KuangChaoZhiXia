using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

/* 
 * 描述：玩家实例数据类，包含燃料，燃料最大值，算力，背包
 * 作者：sine5RAD
 */
public class Player
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

    private GPUBase _gpu; //玩家当前使用的GPU
    public GPUBase GPU
    {
        get { return _gpu; }
        set
        {
            value.Temperature = _gpu.Temperature; // 保持温度不变
            _gpu.Temperature = 0; // 设置旧GPU温度为0
            AddItem(_gpu); // 将旧的GPU放回背包
            _gpu = value; // 设置新的GPU
        }
    }

    private RadiatorBase _radiator; //玩家当前使用的散热器
    public RadiatorBase Radiator
    {
        get { return _radiator; }
        set
        {
            AddItem(_radiator); // 将旧的散热器放回背包
            _radiator = value; // 设置新的散热器
        }
    }
    public float CalcPower {  get { return GPU.CalcPower; } }

    /// <summary>
    /// 每秒总散热效率
    /// </summary>
    public float HeatDissipationEfficiencyPerSecond
    {
        get
        {
            return GPU.HeatDissipationRate * (Radiator.HeatDissipationEfficiency + 3.0f);
        }
    }

    public void UpdatePerFrame()
    {
        GasVal -= Player.GasDissipationRate * Time.deltaTime;
        PlayerRushCoolDown -= Time.deltaTime;
        GPU.Temperature -= HeatDissipationEfficiencyPerSecond * Time.deltaTime;
    }

    public event UnityAction<Player> OnPlayerMoving; //玩家移动时触发事件

    public void Move()
    {
        OnPlayerMoving?.Invoke(this); // 触发玩家移动事件
    }

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
        _bag.AddItem(item);
        OnPlayerMoving += item.OnPlayerMoving; // 订阅玩家移动事件
    }
    /// <summary>
    /// 从背包中移除物品
    /// </summary>
    /// <param name="index"></param>
    public void RemoveItem(int index)
    {
        _bag.RemoveItem(index);
        OnPlayerMoving -= _bag.items[index].OnPlayerMoving; // 取消订阅玩家移动事件
    }
    public Player()
    {
        _basicGasLimit = 1000;
        _gpu = new AMDRX550(); // 默认使用AMD RX 550
        _radiator = new AirCooledRadiator(); // 默认使用风冷散热器
        _gasVal = 1000;
        _basicRushCoolDown = 5;
        _playerRushCoolDown = 0;
    }
}
