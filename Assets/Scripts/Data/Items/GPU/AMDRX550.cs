using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：
 * 作者：sine5RAD
 */
public class AMDRX550 : GPUBase
{
    public AMDRX550() : base()
    {
        _name = "AMD RX 550";
        _baseHeatDissipationEfficiency = 1f;
        _baseCalcPower = 100f; // 基础算力为100
    }
}
