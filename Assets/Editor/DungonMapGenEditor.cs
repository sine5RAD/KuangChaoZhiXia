using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/* 
 * 描述：随机地牢调试工具
 * 作者：sine5RAD
 */

[CustomEditor(typeof(DungonMapGen))]
public class DungonMapGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        if (GUILayout.Button("GenerateMap"))
        {
            ((DungonMapGen)target).GenerateMap();
        }
        if (GUILayout.Button("CleanTileMap"))
        {
            ((DungonMapGen)target).CleanTileMap();
        }
    }
}
