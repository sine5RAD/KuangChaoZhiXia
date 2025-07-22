using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 描述：WASD控制玩家移动
 * 作者：sine5RAD
 */
public class PlayerController : MonoBehaviour
{
    public float speed;
    public RectTransform playerTransform;

    private void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerTransform.position += speed * Vector3.up * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerTransform.position += speed * Vector3.left * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerTransform.position += speed * Vector3.down * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTransform.position += speed * Vector3.right * Time.deltaTime;
        }
    }
}
