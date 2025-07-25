using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * 描述：玩家数据类，包含燃料，燃料最大值，算力，
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
    private float _basicGasLimit;
    public float GasLimit { get { return _basicGasLimit; }}
    private int _basicCalcPower;
    public int CalcPower {  get { return _basicCalcPower; }}
    public static float GasDissipationRate
    {
        get
        {
            switch(SceneManager.GetActiveScene().name)
            {
                case "HallScene":
                case "HomeScene":
                    return 0.5f;
                default:
                    return 1;
            }
        }
    }
    public Player()
    {
        _basicGasLimit = 1000;
        _basicCalcPower = 50;
        _gasVal = 1000;
    }
}
