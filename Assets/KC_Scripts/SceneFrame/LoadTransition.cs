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
        [LabelText("������")]
        public Slider progressBar;
        [LabelText("�����ı�")]
        public TMP_Text progressText;
        [LabelText("��ʾ�ı�")]
        public TMP_Text loadingTipText;

        [Header("Settings")]

        [LabelText("��С����ʱ��")]
        public float minLoadTime = 0.7f;

        public string[] loadingTips = {
            "��ʾ1111...",
            "��ʾ222...",
            "��ʾ3333...",
            "��ʾ444..."
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
            // �����ʾһ��������ʾ
            if (loadingTipText != null && loadingTips.Length > 0)
            {
                loadingTipText.text = loadingTips[Random.Range(0, loadingTips.Length)];
            }

            // ������������
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
            // ��ʼ�첽����
            loadingOperation = SceneManager.LoadSceneAsync(sceneName);
            loadingOperation.allowSceneActivation = false;

            // �ȴ���С����ʱ��
            while (Time.time - loadingStartTime < minLoadTime || loadingOperation.progress < 0.9f)
            {
                // ������ؽ��ȣ�0-0.9ӳ�䵽0-1��
                float progress = Mathf.Clamp01(loadingOperation.progress / 0.9f);

                // ������С����ʱ�䣬ƽ��������ʾ
                float timeProgress = Mathf.Clamp01((Time.time - loadingStartTime) / minLoadTime);
                float displayProgress = Mathf.Min(progress, timeProgress);

                UpdateProgressUI(displayProgress);
                yield return null;
            }

            // ȷ��������ʾΪ100%
            UpdateProgressUI(1f);

            // ������ɣ��ȴ��û����������ӳ�
            yield return new WaitForSeconds(0.5f);

            // ����������
            isLoadingComplete = true;
            loadingOperation.allowSceneActivation = true;

            // �ȴ�������ȫ����
            while (!loadingOperation.isDone)
            {
                yield return null;
            }

            // ����������ɺ����EnterScene
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

        // ��ѡ�������û������Ļ��ǰ���볡������������Сʱ���
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