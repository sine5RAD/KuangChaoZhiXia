using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/* 
 * 描述：一些调试命令
 * 作者：sine5RAD
 */
public class GMCmd : MonoBehaviour
{
    [MenuItem("GMCmd/读取表格")]
    public static void ReadBagTable()
    {
        BagTable bagTable = Resources.Load<BagTable>("Data/BagItems/BagTable");
        foreach(var item in bagTable.bagItems)
        {
            Debug.Log($"name: {item.itemName}\ndescreption: {item.itemDescreption}");
        }
    }
}
