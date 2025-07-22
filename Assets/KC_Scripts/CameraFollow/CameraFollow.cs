using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KCGame
{

    /* 
     * 描述：相机跟随人物移动
     * 作者：sine5RAD
     */
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float smoothing;
        // Start is called before the first frame update
        void Start()
        {
        }

        private void LateUpdate()
        {
            if (target != null)
            {
                if (transform.position != target.position)
                {
                    Vector3 targetPos = target.position;
                    transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}