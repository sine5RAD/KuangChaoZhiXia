using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：BTC数据类
 * 作者：sine5RAD
 */
public class BitCoin : CoinBase
{

    public BitCoin():base()
    {
        _name = "比特币";
        _weight = BagInfoSearcher.Instance.GetInfoFromName(_name).itemWeight;
        _value = BagInfoSearcher.Instance.GetInfoFromName(_name).itemValue;
    }
}
