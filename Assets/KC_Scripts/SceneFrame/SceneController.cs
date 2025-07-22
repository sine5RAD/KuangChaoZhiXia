using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KCGame
{
    public class SceneController
    {
        private static SceneController _instance;
        public static SceneController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SceneController();
                    return _instance;
                }
                return _instance;
            }
        }
        protected SceneController()
        {
            dictScene = new Dictionary<string, SceneBase>();
        }

        public Dictionary<string, SceneBase> dictScene;

        /// <summary>
        /// 加载一个场景
        /// </summary>
        /// <param name="name">目标场景名称</param>
        /// <param name="sceneBace">目标场景scene</param>
        public void LoadScene(string name, SceneBase sceneBace)
        {
            if (!dictScene.ContainsKey(name))
                dictScene.Add(name, sceneBace);
            if (dictScene.ContainsKey(SceneManager.GetActiveScene().name))
                dictScene[SceneManager.GetActiveScene().name].ExitScene();
            else Debug.LogError($"SceneControl未记录{SceneManager.GetActiveScene().name}!");

            #region pop()
            UIManager.Instance.Clear();
            #endregion
            SceneManager.LoadScene(name);
            SceneManager.sceneLoaded += sceneBace.EnterScene;

        }
    }

}