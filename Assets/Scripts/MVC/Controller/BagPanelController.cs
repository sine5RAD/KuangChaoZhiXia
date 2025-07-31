using KCGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* 
 * 描述：局内背包控制器
 * 作者：sine5RAD
 */
public class BagPanelController : KCGame.KCSingletonMonoBehaviour<BagPanelController>
{
    private BagPanel _bagPanel;
    private BagPanelModel _bagPanelModel;

    public GameObject itemViewContent, itemPrefab;
    private List<GameObject> _itemGOList = new List<GameObject>();
    private Dictionary<GameObject, int> _gOIndex = new Dictionary<GameObject, int>();
    private GameObject SelectedGO;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Rua!");
        if(UIManager.Instance.CurrentPanel is BagPanel)
        {
            _bagPanel = UIManager.Instance.CurrentPanel as BagPanel;
        }
        else
        {
            Debug.LogError("当前界面不是BagPanel");
        }
        _bagPanelModel = new BagPanelModel();
        _bagPanelModel.AddListener(_bagPanel.Update);
        ShowBagItem();
    }

    private void ShowBagItem()
    {
        #region 实例化背包物体
        if(_bagPanelModel == null)
        {
            Debug.LogWarning("背包数据未初始化");
            return;
        }
        for (int i = 0; i < _bagPanelModel.Bag.items.Count; i++)
        {
            GameObject newItem = GameObject.Instantiate(itemPrefab);
            _gOIndex.Add(newItem, i);
            _itemGOList.Add(newItem);
            newItem.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = _bagPanelModel.Bag.items[i].Name;
            newItem.transform.SetParent(itemViewContent.transform, false);
        }
        #endregion
    }

    public void SelectItem(GameObject go)
    {
        if(SelectedGO != null)
        {
            SelectedGO.GetComponentInChildren<Outline>().enabled = false;
        }
        go.GetComponentInChildren<Outline>().enabled = true;
        SelectedGO = go;
        _bagPanelModel.SelectedItem = _bagPanelModel.Bag.items[_gOIndex[go]];
    }

    private void OnEnable()
    {
        foreach (var i in _itemGOList)
        {
            GameObject.Destroy(i);
        }
        _itemGOList.Clear();
        _gOIndex.Clear();
        ShowBagItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
