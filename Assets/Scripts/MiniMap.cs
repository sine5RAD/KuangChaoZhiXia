using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/*
 * 需要在小地图上显示的对象，需要添加一个子对象，并且把子对象的layer设置为Minimap
 * 例如希望在小地图上显示玩家为“玩家”字样，则在玩家预制体下添加一个画布 并且设置画布的层为MiniMap 然后在画布上添加一个txt组件
 */



public class MiniMap : MonoBehaviour
{



    public Camera MiniMapCamera;


    //调整MiniMapCamera的缩放
    public void MiniMap_ChangeZoom()
    {

    }

}
