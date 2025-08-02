using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：背包物品静态数据
 * 作者：sine5RAD
 */
[CreateAssetMenu(menuName = "KCGame/BagTable", fileName = "BagTable")]
public class BagTable : ScriptableObject
{
    public List<BagItem> bagItems = new List<BagItem>();
}

[System.Serializable]
public class BagItem
{
    public string itemName;
    public string itemDescreption;

    public enum BagItemType
    {
        Null,
        Coin,
        Ore,
        GPU,
        Radiator,
        Prop
    }

    public BagItemType itemType;
}