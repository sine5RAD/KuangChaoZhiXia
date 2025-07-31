using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：查询物品信息的单例模式
 * 作者：sine5RAD
 */
public class BagInfoSearcher : KCGame.KCSingleton<BagInfoSearcher>
{
    private Dictionary<string, BagItem> _itemInfoDict = new Dictionary<string, BagItem>();
    public BagInfoSearcher() : base()
    {
        BagTable bagTable = Resources.Load<BagTable>("Data/BagItems/BagTable");
        foreach(var i in bagTable.bagItems)
        {
            if (!_itemInfoDict.ContainsKey(i.itemName))
            {
                _itemInfoDict.Add(i.itemName, i);
            }
            else
            {
                Debug.LogError($"Duplicate item ID found: {i.itemName}");
            }
        }
    }

    public BagItem GetInfoFromName(string itemName)
    {
        if (_itemInfoDict.TryGetValue(itemName, out BagItem item))
        {
            return item;
        }
        else
        {
            Debug.LogError($"Item not found: {itemName}");
            return null;
        }
    }
}
