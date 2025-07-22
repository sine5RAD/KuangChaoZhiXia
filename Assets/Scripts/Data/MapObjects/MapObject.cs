using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：地图物体数据类
 * 作者：sine5RAD
 */
public class MapObject : ScriptableObject
{
    private string _name;
    public string Name { get { return _name; } }
    private int _width, _height;
    public int Width { get { return _width; } }
    public int Height { get { return _height; } }
    private bool _isInteractable;
    public bool IsInteractable {  get { return _isInteractable; } }
    private bool _isAccessible;
    public bool IsAccessible {  get { return _isAccessible; } }
}
