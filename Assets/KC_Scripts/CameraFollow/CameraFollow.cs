using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace KCGame
{

    public class CameraFollow : MonoBehaviour
    {


        public CinemachineVirtualCamera VirtualCamera;

        public void Start()
        {
            Transform tr_Player = GameObject.FindWithTag("Player").transform;
            VirtualCamera.Follow = tr_Player;
            VirtualCamera.LookAt = tr_Player;

        }

    }

}