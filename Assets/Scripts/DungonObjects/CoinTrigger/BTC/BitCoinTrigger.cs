using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：比特币脚本
 * 作者：sine5RAD
 */
public class BitCoinTrigger : CoinTriggerBase
{
    private void Awake()
    {
        Init();
        transform.parent.GetComponent<PushableTrigger>().BagLocalItemBase = coinInfo;
    }
    public override void Init()
    {
        if (coinInfo != null)
            return;
        coinInfo = new BitCoin();
    }
}
