using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：货币基类
 * 作者：sine5RAD
 */
public class CoinBase : BagLocalItemBase
{
    public CoinBase()
    {
        _itemType = BagItem.BagItemType.Coin;
    }
    /// <summary>
    /// 每移动1m升高温度
    /// </summary>
    public virtual float TemperatureVelocity(Player player)
    {
        return 1.0f;// 默认移动升温速率
    }
    public override void OnPlayerMoving(Player player)
    {
    }
}
