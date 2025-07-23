using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KCGame
{
    /// <summary>
    /// UI基类
    /// </summary>
    public class BasePanel
    {
        public UIType uiType;
        public bool activeFlag;
        private bool _hasDestroyed;

        /// <summary>
        /// UI在场景中对应的GameObject
        /// </summary>
        public GameObject activeObj;

        public BasePanel(UIType uIType)
        {
            this.uiType = uIType;
        }

        /// <summary>
        /// 在UI入栈时调用
        /// </summary>
        public virtual void OnStart()
        {
            if (_hasDestroyed) return;
            UIMethods.Instance.GetComponent<CanvasGroup>(activeObj).interactable = true;
            activeFlag = true;
            _hasDestroyed = false;
        }

        /// <summary>
        /// 在UI被启用时调用
        /// </summary>
        public virtual void OnEnable()
        {
            if (_hasDestroyed) return;
            UIMethods.Instance.GetComponent<CanvasGroup>(activeObj).interactable = true;
            activeFlag = true;
            _hasDestroyed = false;
        }

        /// <summary>
        /// 在UI被关闭时调用
        /// </summary>
        public virtual void OnDisable()
        {
            if (_hasDestroyed) return;
            UIMethods.Instance.GetComponent<CanvasGroup>(activeObj).interactable = false;
            activeFlag = false;
            _hasDestroyed = false;
        }

        /// <summary>
        /// 在UI被从内存中销毁时调用
        /// </summary>
        public virtual void OnDestroy()
        {
            if (_hasDestroyed) return;
            UIMethods.Instance.GetComponent<CanvasGroup>(activeObj).interactable = false;
            activeFlag = false;
            _hasDestroyed = true;
        }

        /// <summary>
        /// 当UI被置于栈顶或者别的什么需要刷新的情况下调用
        /// </summary>
        public virtual void OnUpdate()
        {
            if (_hasDestroyed) return;
        }
    }

}