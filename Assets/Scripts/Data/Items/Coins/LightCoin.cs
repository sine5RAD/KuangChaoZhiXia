using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：LTC数据类
 * 作者：sine5RAD
 */
public class LightCoin : CoinBase
{
    public LightCoin()
    {
        _itemType = BagItem.BagItemType.Coin;
        _name = "莱特币";
        _weight = 150f;
        _value = 0.5f;
    }
    public override float TemperatureVelocity(Player player)
    {
        return 4f * (1 + player.GPU.Temperature / 50);
    }
    public override void OnPlayerMoving(Player player)
    {
        player.GPU.Temperature += TemperatureVelocity(player) * Time.deltaTime;
    }
}
