using JetBrains.Annotations;
using KCGame;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/* 
 * 描述：WASD控制玩家移动
 * 作者：sine5RAD
 */
public class PlayerController : MonoBehaviour
{
    public float speed;
    public RectTransform playerTransform;
    public GameObject pressETip;
    private event UnityAction onPressE;
    private bool _hasInteractItem;
    private bool _isMovingLocked = false;

    private void Start()
    {
    }

    /// <summary>
    /// 清空按下E键后执行的函数列表，在玩家离开trigger时触发
    /// </summary>
    public void RemoveInteractItem()
    {
        _hasInteractItem = false;
        onPressE = null;
        pressETip.SetActive(false);
    }
    /// <summary>
    /// 设置按下E键后执行的函数
    /// </summary>
    /// <param name="func"></param>
    public void SwitchInteractItem(UnityAction func)
    {
        RemoveInteractItem();
        _hasInteractItem = true;
        onPressE += func;
        pressETip.SetActive(true);
    }

    void FixedUpdate()
    {
        if(!InputUtility.Instance.IsMovingLocked)
        {
            Vector3 direction = Vector3.zero;
            if (Input.GetKey(KeyCode.W) && !_isMovingLocked)
            {
                direction += Vector3.up;
            }
            if (Input.GetKey(KeyCode.A) && !_isMovingLocked)
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KeyCode.S) && !_isMovingLocked)
            {
                direction += Vector3.down;
            }
            if (Input.GetKey(KeyCode.D) && !_isMovingLocked)
            {
                direction += Vector3.right;
            }
            playerTransform.position += direction.normalized * speed * Time.deltaTime;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_hasInteractItem)
            {
                onPressE?.Invoke();
            }
        }
    }
}
