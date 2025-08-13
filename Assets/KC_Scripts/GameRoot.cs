using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KCGame
{
    public class GameRoot : MonoBehaviour
    {

        //杂项通用资源 暂时先放这
        [SerializeField] public SO_KCCommonAssets KCAssets;



        private static GameRoot _instance;
        public static GameRoot Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogWarning("GameRoot没有绑定实例!");
                    return _instance;
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            UIManager.Instance.canvasObj = UIMethods.Instance.FindCanvas();

            #region 推入开始游戏界面
            UIManager.Instance.Push(new MainMenuPanel(MainMenuPanel.uIType));
            #endregion
            SceneController.Instance.dictScene.Add("MainMenuScene", new MainMenuScene());
        }

        private void Update()
        {
            UIManager.Instance.CurrentPanel?.OnUpdate();
        }
    }

}