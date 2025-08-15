using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：LTC数据类
 * 作者：sine5RAD
 */
public class LightCoin : CoinBase
{
    public LightCoin():base()
    {
        _name = "莱特币";
        _weight = BagInfoSearcher.Instance.GetInfoFromName(_name).itemWeight;
        _value = BagInfoSearcher.Instance.GetInfoFromName(_name).itemValue;
    }
}
