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
}
