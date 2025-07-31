using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* 
 * 描述：局内背包系统
 * 作者：sine5RAD
 */
public class BagLocalData : KCGame.KCSingleton<BagLocalData>
{
    public List<BagLocalItemBase> items;       //物品列表
    public event UnityAction onPlayerMoving;   //玩家移动时触发

    public BagLocalData()
    {
        items = new List<BagLocalItemBase>();
    }
}

public class BagLocalItemBase
{
    protected BagItem.BagItemType _itemType;
    public BagItem.BagItemType ItemType
    {
        get { return _itemType; }
    }

    protected string _name;
    public string Name
    {
        get { return _name; }
    }
    protected float _weight;
    public virtual float Weight
    {
        get { return _weight; }
    }
    protected float _value;
    public virtual float Value
    {
        get { return _value; }
    }

    public virtual void OnPlayerMoving()
    {

    }
}