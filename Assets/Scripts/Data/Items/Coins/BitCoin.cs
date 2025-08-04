using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：BTC数据类
 * 作者：sine5RAD
 */
public class BitCoin : CoinBase
{

    public BitCoin()
    {
        _itemType = BagItem.BagItemType.Coin;
        _name = "比特币";
        _weight = 300f;
        _value = 1f;
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
