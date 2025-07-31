using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
 * 描述：点选背包物体
 * 作者：sine5RAD
 */
public class SelectBagItem : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => 
        {
            BagPanelController.Instance.SelectItem(transform.parent.gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
