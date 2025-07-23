using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KCGame
{
    public class LoadTransition : MonoBehaviour
    {
        public static LoadTransition Instance { get; private set; }


        [Header("UI Elements")]
        [LabelText("进度条")]
        public Slider progressBar;
        [LabelText("进度文本")]
        public TMP_Text progressText;
        [LabelText("提示文本")]
        public TMP_Text loadingTipText;

        [Header("Settings")]

        [LabelText("最小加载时间")]
        public float minLoadTime = 0.7f;

        public string[] loadingTips = {
            "提示1111...",
            "提示222...",
            "提示3333...",
            "提示444..."
        };

        private AsyncOperation loadingOperation;
        private float loadingStartTime;
        private bool isLoadingComplete;
        private SceneBase targetSceneBase;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // 随机显示一条加载提示
            if (loadingTipText != null && loadingTips.Length > 0)
            {
                loadingTipText.text = loadingTips[Random.Range(0, loadingTips.Length)];
            }

            // 启动加载流程
            StartLoading(SceneController.Instance.TargetSceneName, SceneController.Instance.TargetSceneBase);
        }


        public void StartLoading(string sceneName, SceneBase sceneBase)
        {
            targetSceneBase = sceneBase;
            loadingStartTime = Time.time;
            isLoadingComplete = false;

            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            // 开始异步加载
            loadingOperation = SceneManager.LoadSceneAsync(sceneName);
            loadingOperation.allowSceneActivation = false;

            // 等待最小加载时间
            while (Time.time - loadingStartTime < minLoadTime || loadingOperation.progress < 0.9f)
            {
                // 计算加载进度（0-0.9映射到0-1）
                float progress = Mathf.Clamp01(loadingOperation.progress / 0.9f);

                // 考虑最小加载时间，平滑进度显示
                float timeProgress = Mathf.Clamp01((Time.time - loadingStartTime) / minLoadTime);
                float displayProgress = Mathf.Min(progress, timeProgress);

                UpdateProgressUI(displayProgress);
                yield return null;
            }

            // 确保进度显示为100%
            UpdateProgressUI(1f);

            // 加载完成，等待用户点击或短暂延迟
            yield return new WaitForSeconds(0.5f);

            // 允许场景激活
            isLoadingComplete = true;
            loadingOperation.allowSceneActivation = true;

            // 等待场景完全加载
            while (!loadingOperation.isDone)
            {
                yield return null;
            }

            // 场景加载完成后调用EnterScene
            if (targetSceneBase != null)
            {
                targetSceneBase.EnterScene();
            }
        }

        private void UpdateProgressUI(float progress)
        {
            if (progressBar != null)
            {
                progressBar.value = progress;
            }

            if (progressText != null)
            {
                progressText.text = $"{(progress * 100):0}%";
            }
        }

        // 可选：允许用户点击屏幕提前进入场景（在满足最小时间后）
        private void Update()
        {
            if (!isLoadingComplete && Time.time - loadingStartTime >= minLoadTime && Input.GetMouseButtonDown(0))
            {
                isLoadingComplete = true;
                loadingOperation.allowSceneActivation = true;
            }
        }
    }
}