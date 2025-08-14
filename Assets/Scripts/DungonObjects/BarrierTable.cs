using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：地图障碍物静态数据
 * 作者：sine5RAD
 */

[CreateAssetMenu(menuName = "KCGame/BarrierTable", fileName = "BarrierTable")]
public class BarrierTable : ScriptableObject
{
    public List<BarrierItem> barrierItems = new List<BarrierItem>();
}
[System.Serializable]
public class BarrierItem
{
    public string name;
    public int deltaX, deltaY;
}
