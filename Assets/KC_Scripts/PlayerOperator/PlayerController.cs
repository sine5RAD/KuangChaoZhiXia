using KCGame;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IMapRole
{
    public GameObject pressETip;
    private event UnityAction OnPressE;
    private bool _hasInteractItem;
    private Vector3Int direction = Vector3Int.zero;
    private Coroutine _rushCoroutine;
    private bool _isRushing = false;
    private Grid _mapGrid;

    [ShowInInspector]
    public Vector2Int CellPos { get; set; }

    [ShowInInspector]
    public MapItemType RoleType { get; set; } = MapItemType.玩家;

    private void Start()
    {
        _mapGrid = GameMapUnit.Instance.MapGrid;
        GameMapUnit.Instance.Register(this);
        CellPos = GameMapUnit.Instance.Fix_WorldToCell(transform.position);
    }

    public MapRoleProp MapRegister()
    {
        return new MapRoleProp(transform.position, MapItemType.玩家);
    }

    public void MoveTo(Vector2Int newCellPos)
    {
        CellPos = newCellPos;
        transform.position = GameMapUnit.Instance.Fix_CellToWrold(newCellPos);
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
    public void SwitchInteractItem(UnityAction func)
    {
        RemoveInteractItem();
        _hasInteractItem = true;
        OnPressE += func;
        pressETip.SetActive(true);
    }

    void FixedUpdate()
    {
        if (!InputUtility.Instance.IsMovingLocked &&
            !_isRushing &&
            !PlayerUIPanelController.Instance.player.GPU.IsOverload &&
            PlayerUIPanelController.Instance.player.CurrentMovingCooldown == 0)
        {
            // 收集输入方向
            if (Input.GetKey(KeyCode.W)) direction += Vector3Int.up;
            if (Input.GetKey(KeyCode.A)) direction += Vector3Int.left;
            if (Input.GetKey(KeyCode.S)) direction += Vector3Int.down;
            if (Input.GetKey(KeyCode.D)) direction += Vector3Int.right;

            if (direction != Vector3.zero && CanMove())
            {
                // 标准化方向向量
                if (Mathf.Abs(direction.x) > 0) direction.y = 0;
                else if (Mathf.Abs(direction.y) > 0) direction.x = 0;

                // 转换为MoveDir枚举
                MoveDir moveDir = GetMoveDir(direction);

                // 尝试移动
                if (GameMapUnit.Instance.TryMove(this, moveDir, out Vector3 newPos))
                {
                    PlayerUIPanelController.Instance.player.Move();
                }

                // 冲刺检测
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (!InputUtility.Instance.IsMovingLocked && PlayerUIPanelController.Instance.InvokeRushSkill())
                    {
                        Debug.Log("冲刺！");
                        _rushCoroutine = StartCoroutine(RushSkill(moveDir));
                    }
                }
            }

            direction = Vector3Int.zero;
        }
    }

    private MoveDir GetMoveDir(Vector3Int dir)
    {
        if (dir.x > 0) return MoveDir.右;
        if (dir.x < 0) return MoveDir.左;
        if (dir.y > 0) return MoveDir.上;
        return MoveDir.下;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            StopMoving();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _hasInteractItem)
        {
            OnPressE?.Invoke();
        }
    }

    /// <summary>
    /// 冲刺技能
    /// </summary>
    private IEnumerator RushSkill(MoveDir dir)
    {
        _isRushing = true;

        for (int i = 0; i < 3; i++)
        {
            // 直接使用地图系统的TryMove方法
            if (!GameMapUnit.Instance.TryMove(this, dir, out _))
            {
                // 遇到障碍物停止冲刺
                break;
            }

            yield return new WaitForSeconds(0.1f);
        }

        _isRushing = false;
    }

    public void StopMoving()
    {
        if (_isRushing && _rushCoroutine != null)
        {
            StopCoroutine(_rushCoroutine);
            _isRushing = false;
        }
    }

    public bool CanMove()
    {
        return true;
    }
}