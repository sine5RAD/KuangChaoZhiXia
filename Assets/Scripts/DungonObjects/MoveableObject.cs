using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：可移动物体基类
 * 作者：sine5RAD
 */
public class MoveableObject : MonoBehaviour
{



    protected bool _isMoving = false;
    protected Grid _mapGrid;
    protected Coroutine _moveCoroutine;

    private void Awake()
    {
        _mapGrid = GameObject.Find("Grid").GetComponent<Grid>();
        if (_mapGrid == null)
        {
            Debug.LogError("Grid component not found in the scene.");
        }
    }

   

    protected IEnumerator Move(Vector3 from, Vector3 to, float time)
    {
        _isMoving = true;
        float dt = 0;
        while (dt <= time)
        {
            dt += Time.deltaTime;
            transform.position += (to - from) * (Time.deltaTime / time);
            yield return null;
        }
        transform.position = _mapGrid.CellToWorld(_mapGrid.WorldToCell(transform.position)) + new Vector3(0.64f, 0.64f, 0);
        _isMoving = false;
    }
    public virtual void StopMoving()
    {
        if (_isMoving)
        {
            if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
            transform.position = _mapGrid.CellToWorld(_mapGrid.WorldToCell(transform.position)) + new Vector3(0.64f, 0.64f, 0);
            _isMoving = false;
        }
    }
}
