using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadingScene : MonoBehaviour
{
    private AsyncOperationHandle m_SceneHandle;

    Image fullLoadingBar;
    float waitTime = 5f;
    float timer = 0.1f;
    private void OnEnable()
    {
        m_SceneHandle = Addressables.DownloadDependenciesAsync("DemoScene");
        m_SceneHandle.Completed += OnSceneLoaded;
    }
    private void OnDisable()
    {
        m_SceneHandle.Completed -= OnSceneLoaded;
    }
    private void OnSceneLoaded(AsyncOperationHandle obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {
            Addressables.LoadSceneAsync("DemoScene", LoadSceneMode.Single, true);
        }
    }
    private void Awake()
    {
        Time.timeScale = 1f;
    }
    void Start()
    {
        fullLoadingBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        fullLoadingBar.fillAmount = m_SceneHandle.PercentComplete;
    }
}
