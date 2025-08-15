using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：莱特币脚本
 * 作者：sine5RAD
 */
public class LightCoinTrigger : CoinTriggerBase
{

    private void Awake()
    {
        Init();
        gameObject.GetComponentInChildren<PushableTrigger>().BagLocalItemBase = coinInfo;
    }
    public override void Init()
    {
        if(coinInfo != null)
            return;
        coinInfo = new LightCoin();
    }
}
