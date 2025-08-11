using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：GPU基类
 * 作者：sine5RAD
 */
public class GPUBase: BagLocalItemBase
{
    protected bool _isOverload; //是否过载
    public bool IsOverload
    {
        get { return _isOverload; }
    }
    protected float _baseCalcPower; //基础算力
    public virtual float CalcPower
    {
        get
        {
            if(0 <= Temperature && Temperature < 60)
            {
                return _baseCalcPower;
            }
            else if(Temperature < 70)
            {
                return _baseCalcPower * 0.9f;
            }
            else if(Temperature < 80)
            {
                return _baseCalcPower * 0.75f;
            }
            else if(Temperature <= 100)
            {
                return _baseCalcPower * 0.4f;
            }
            else
            {
                throw new System.Exception("温度怎么超100了？");
            }
        }
    }

    public string Condition
    {
         get
        {
            if(IsOverload) return "过载";
            if (Temperature < 60) return "预热";
            else if (Temperature < 70) return "最佳效能";
            else if (Temperature < 80) return "过热风险";
            else if (Temperature <= 100) return "危险";
            else return "异常";
        }
    }

    protected float _temperature; //温度
    public virtual float Temperature
    {
        get { return _temperature; }
        set
        {
            _temperature = value;
            if (_temperature < 0)
            {
                _temperature = 0;
                _isOverload = false; // 温度降到0时不再过载
            }
            if (_temperature > 100)
            {
                _temperature = 100; // 温度上限为100
                _isOverload = true; // 温度超过100时过载
            }
        }
    }

    protected float _baseHeatDissipationRate = 1f; //基础散热率
    public virtual float HeatDissipationRate
    {
        get { return _baseHeatDissipationRate; }
    }

    public GPUBase()
    {
        _isOverload = false;
        _itemType = BagItem.BagItemType.GPU;
        _temperature = 0f; // 初始温度为0
    }
}
