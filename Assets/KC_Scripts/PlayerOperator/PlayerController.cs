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
    public GameObject pressETip;
    private event UnityAction onPressE;
    private bool _hasInteractItem;
    private Vector3 direction = Vector3.zero;

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
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3.up;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector3.down;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector3.right;
            }
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftShift) && direction != Vector3.zero)
            {
                if (!InputUtility.Instance.IsMovingLocked)
                {
                    if (PlayerUIPanelController.Instance.InvokeRushSkill())
                    {
                        Debug.Log("冲刺！");
                        StartCoroutine(RushSkill(direction));
                    }
                }
            }
            direction = Vector3.zero;
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
    /// <summary>
    /// 冲刺！冲刺！冲！冲！冲刺！
    /// </summary>
    /// <returns></returns>
    private IEnumerator RushSkill(Vector3 dir)
    {
        InputUtility.Instance.LockMoving();
        float dT = 0;
        speed *= 2;
        while(dT < 0.1f)
        {
            dT += Time.deltaTime;
            transform.position += dir.normalized * speed * Time.deltaTime;
            yield return null;
        }
        speed /= 2;
        InputUtility.Instance.UnlockMoving();
    }
}
