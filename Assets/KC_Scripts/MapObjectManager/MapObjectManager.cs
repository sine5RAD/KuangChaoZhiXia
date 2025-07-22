using Sirenix.OdinInspector.Editor.Validation;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

/* 
 * 描述：地图物体管理器
 * 作者：sine5RAD
 */
public class MapObjectManager : MonoBehaviour
{
    private Dictionary<string, GameObject> _objectsDict = new Dictionary<string, GameObject>();
    public Grid mapGrid;
    public Tilemap tilemap;
    public GameObject buildingObject;
    /// <summary>
    /// 地图缩放，默认为4倍
    /// </summary>
    public float scale = 4f;
    private static readonly string _resPath = "Prefab/Objects/Map";
    void Start()
    {
        var bound = tilemap.cellBounds;
        foreach(var pos in bound.allPositionsWithin)
        {
            var sprite = tilemap.GetSprite(pos);
            if(sprite != null)
            {
                Vector2Int tilePos = new Vector2Int(pos.x, pos.y);
                GameObject newGO;
                if (!_objectsDict.ContainsKey(sprite.name))
                {
                    Debug.Log($"{_resPath}/{sprite.name}");
                    _objectsDict.Add(sprite.name, GameObject.Instantiate(Resources.Load<GameObject>($"{_resPath}/{sprite.name}")));
                }
                Vector3 worldPos = mapGrid.GetCellCenterLocal(new Vector3Int(tilePos.x, tilePos.y, 0)) * scale;
                newGO = GameObject.Instantiate(_objectsDict[sprite.name], worldPos, Quaternion.identity, buildingObject.transform);
            }
        }
        foreach(var i in _objectsDict.Values)
            i.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
