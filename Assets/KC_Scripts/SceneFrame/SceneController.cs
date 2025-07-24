using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

namespace KCGame
{
    public class SceneController:KCSingleton<SceneController>
    {



        public Dictionary<string, SceneBase> dictScene;

        protected override void OnCreated()
        {
            base.OnCreated();
            dictScene = new Dictionary<string, SceneBase>();
        }




        // 目标场景信息
        public string TargetSceneName { get; private set; }
        public SceneBase TargetSceneBase { get; private set; }

      


        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="name">场景名称</param>
        /// <param name="sceneBase">场景脚本</param>
        /// <param name="hardLoading">硬加载（为true则不使用过度场景）</param>
        public void LoadScene(string name, SceneBase sceneBase,bool hardLoading = false)
        {
            // 存储目标场景信息
            TargetSceneName = name;
            TargetSceneBase = sceneBase;

            if (!dictScene.ContainsKey(name))
            {
                dictScene.Add(name, sceneBase);
            }

            // 退出当前场景
            if (dictScene.ContainsKey(SceneManager.GetActiveScene().name))
            {
                dictScene[SceneManager.GetActiveScene().name].ExitScene();
            }
            else
            {
                Debug.LogWarning($"SceneController未记录{SceneManager.GetActiveScene().name}场景");
            }

            // 清理UI
            UIManager.Instance.Clear();

            if (hardLoading)
            {
                SceneManager.LoadScene(name);
                SceneManager.sceneLoaded += sceneBase.EnterScene;
            }
            else
            {
                LoadTransition.sceneLoaded = sceneBase.EnterScene;
                // 加载过渡场景
                SceneManager.LoadScene(KCConstant.过度场景名称);
            }
        }







    }

}