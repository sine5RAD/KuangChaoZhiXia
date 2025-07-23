using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KCGame
{
    /* 
     * 描述：有关键盘输入的一些脚本
     * 作者：sine5RAD
     */
    public class InputUtility
    {
        private static InputUtility _instance;
        public static InputUtility Instance
        {
            get
            {
                if (_instance == null) _instance = new InputUtility();
                return _instance;
            }
        }

        private bool _isMovingLocked;
        public bool IsMovingLocked
        {
            get { return _isMovingLocked; }
        }

        /// <summary>
        /// 禁止玩家角色移动
        /// </summary>
        public void LockMoving() { _isMovingLocked = true; }
        /// <summary>
        /// 解锁玩家角色移动
        /// </summary>
        public void UnlockMoving() { _isMovingLocked = false; }
        protected InputUtility()
        {
            _isMovingLocked = false;
        }
    }

}