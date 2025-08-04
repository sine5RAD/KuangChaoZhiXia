using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：散热器基类
 * 作者：sine5RAD
 */
public class RadiatorBase : BagLocalItemBase
{
    public RadiatorBase()
    {
        _itemType = BagItem.BagItemType.Radiator;
    }
    protected float _baseHeatDissipationEfficiency = 1f; // 基础散热效率
    public float HeatDissipationEfficiency
    {
        get { return _baseHeatDissipationEfficiency; }
    }
}
