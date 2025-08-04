using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：
 * 作者：sine5RAD
 */
public class AirCooledRadiator : RadiatorBase
{
    public AirCooledRadiator(): base()
    {
        _name = "风冷散热器";
        _baseHeatDissipationEfficiency = 3f; // 风冷散热器的基础散热效率
        _weight = 0.5f; // 风冷散热器的重量
        _value = 50f; // 风冷散热器的价值
    }
}
