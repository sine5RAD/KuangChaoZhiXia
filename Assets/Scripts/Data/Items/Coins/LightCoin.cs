using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：LTC数据类
 * 作者：sine5RAD
 */
public class LightCoin : BagLocalItemBase
{
    public LightCoin()
    {
        _itemType = BagItem.BagItemType.Coin;
        _name = "莱特币";
        _weight = 0.5f;
        _value = 0.5f;
    }
    public override void OnPlayerMoving()
    {
        // 在玩家移动时执行的逻辑
        Debug.Log("玩家正在移动，携带的莱特币数量：" + Player.Instance.Bag.items.Count);
    }
}
