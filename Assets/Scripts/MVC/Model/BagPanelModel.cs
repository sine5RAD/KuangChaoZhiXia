using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* 
 * 描述：局内背包界面数据类
 * 作者：sine5RAD
 */
public class BagPanelModel
{
    private BagLocalData _bag;
    public BagLocalData Bag { get { return _bag; } }
    public BagPanelModel()
    {
        _bag = Player.Instance.Bag;
    }

    private BagLocalItemBase _selectedItem = null;
    public BagLocalItemBase SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;
            Update();
        }
    }

    public void AddItem(BagLocalItemBase item)
    {
        Player.Instance.AddItem(item);
        _bag.onPlayerMoving += item.OnPlayerMoving;
    }

    public void RemoveItem(int index)
    {
        _bag.onPlayerMoving -= _bag.items[index].OnPlayerMoving;
        Player.Instance.RemoveItem(index);
    }

    private event UnityAction<BagPanelModel> _onUpdate;
    public void AddListener(UnityAction<BagPanelModel> onUpdate)
    {
        _onUpdate += onUpdate;
    }
    public void RemoveListener(UnityAction<BagPanelModel> onUpdate)
    {
        _onUpdate -= onUpdate;
    }

    private void Update()
    {
        _onUpdate?.Invoke(this);
    }
}
