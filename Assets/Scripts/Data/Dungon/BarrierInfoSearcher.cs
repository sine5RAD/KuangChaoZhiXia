using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：地图障碍物信息查询类
 * 作者：sine5RAD
 */
public class BarrierInfoSearcher : KCGame.KCSingleton<BarrierInfoSearcher>
{
    private Dictionary<string, BarrierItem> _barrierInfoDict;
    public BarrierInfoSearcher() : base()
    {
        _barrierInfoDict = new Dictionary<string, BarrierItem>();
        BarrierTable barrierTable = Resources.Load<BarrierTable>("Data/BarrierItems/BarrierTable");
        foreach (var i in barrierTable.barrierItems)
        {
            if (!_barrierInfoDict.ContainsKey(i.name))
            {
                _barrierInfoDict.Add(i.name, i);
            }
            else
            {
                Debug.LogError($"Duplicate barrier ID found: {i.name}");
            }
        }
    }
    public BarrierItem GetInfoFromName(string barrierName)
    {
        if (_barrierInfoDict.TryGetValue(barrierName, out BarrierItem barrier))
        {
            return barrier;
        }
        else
        {
            return null;
        }
    }
}
