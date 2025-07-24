using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace KCGame
{

    public class CameraFollow : MonoBehaviour
    {


        public CinemachineVirtualCamera VirtualCamera;

        public void SetPlayer(Transform tr_Player)
        {
            VirtualCamera.Follow = tr_Player;
            VirtualCamera.LookAt = tr_Player;
        }

    }

}