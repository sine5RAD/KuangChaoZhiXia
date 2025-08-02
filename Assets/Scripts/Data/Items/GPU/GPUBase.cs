using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：GPU基类
 * 作者：sine5RAD
 */
public class GPUBase: BagLocalItemBase
{
    protected float _baseCalcPower; //基础算力
    public virtual float CalcPower
    {
        get
        {
            if(0 < Temperature && Temperature < 60)
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

    protected float _temperature; //温度
    public virtual float Temperature
    {
        get { return _temperature; }
        set
        {
            _temperature = value;
            if (_temperature < 0) _temperature = 0;
            if (_temperature > 100) _temperature = 100; // 温度上限为100
        }
    }

    protected float _baseHeatDissipationEfficiency = 1f; //基础散热效率
    public virtual float HeatDissipationEfficiency
    {
        get { return _baseHeatDissipationEfficiency; }
    }

    public GPUBase()
    {
        _itemType = BagItem.BagItemType.GPU;
        _temperature = 0f; // 初始温度为0
    }
}
