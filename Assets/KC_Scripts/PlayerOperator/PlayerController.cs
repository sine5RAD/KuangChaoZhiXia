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
public class PlayerController : MoveableObject
{
    public GameObject pressETip;
    private event UnityAction OnPressE;
    private bool _hasInteractItem;
    private Vector3Int direction = Vector3Int.zero;
    private Coroutine _rushCoroutine;

    private bool _isRushing = false;

    private void Start()
    {
    }

    /// <summary>
    /// 清空按下E键后执行的函数列表，在玩家离开trigger时触发
    /// </summary>
    public void RemoveInteractItem()
    {
        _hasInteractItem = false;
        OnPressE = null;
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
        OnPressE += func;
        pressETip.SetActive(true);
    }

    void FixedUpdate()
    {
        if(!InputUtility.Instance.IsMovingLocked && 
           !_isRushing && 
           !PlayerUIPanelController.Instance.player.GPU.IsOverload && 
            PlayerUIPanelController.Instance.player.CurrentMovingCooldown == 0)
        {
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3Int.up;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector3Int.left;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector3Int.down;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector3Int.right;
            }
            if(direction != Vector3.zero)
            {
                Vector3Int pos = _mapGrid.WorldToCell(transform.position);
                PlayerUIPanelController.Instance.player.Move();
                _moveCoroutine = StartCoroutine(Move(transform.position, _mapGrid.CellToWorld(pos + direction) + new Vector3(0.64f, 0.64f, 0), PlayerUIPanelController.Instance.player.CurrentMovingCooldown));
                
            }
            if (Input.GetKey(KeyCode.LeftShift) && direction != Vector3.zero)
            {
                if (!InputUtility.Instance.IsMovingLocked)
                {
                    if (PlayerUIPanelController.Instance.InvokeRushSkill())
                    {
                        Debug.Log("冲刺！");
                        _rushCoroutine = StartCoroutine(RushSkill(direction));
                    }
                }
            }
            direction = Vector3Int.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            StopMoving();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_hasInteractItem)
            {
                OnPressE?.Invoke();
            }
        }
    }
    /// <summary>
    /// 冲刺！冲刺！冲！冲！冲刺！
    /// </summary>
    /// <returns></returns>
    private IEnumerator RushSkill(Vector3Int dir)
    {
        _isRushing = true;
        for(int i = 0; i < 3; i++)
        {
            Vector3Int pos = _mapGrid.WorldToCell(transform.position);
            _moveCoroutine = StartCoroutine(Move(transform.position, _mapGrid.CellToWorld(pos + dir) + new Vector3(0.64f, 0.64f, 0), 0.1f));
            yield return new WaitForSeconds(0.1f);
        }
        _isRushing = false;
    }

    public override void StopMoving()
    {
        if (_isMoving || _isRushing)
        {
            if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
            if (_rushCoroutine != null) StopCoroutine(_rushCoroutine);
            _isMoving = false;
            _isRushing = false;
            transform.position = _mapGrid.CellToWorld(_mapGrid.WorldToCell(transform.position)) + new Vector3(0.64f, 0.64f, 0);
        }
    }
}
