using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.IO;

public class LoadingScene : MonoBehaviour
{
    private AsyncOperationHandle m_SceneHandle;
    private AsyncOperationHandle m_loadAllie;
    public Text LoadingText;
    AsyncOperationHandle<long> getDownloadSize;
    AsyncOperationHandle downloadDependencies;
    DownloadStatus m_Status;
    Image fullLoadingBar;
    float waitTime = 2.5f;
    float timer = 0.1f;
    private void GetNewScene()
    {
        m_SceneHandle = Addressables.DownloadDependenciesAsync("DemoScene");
        m_SceneHandle.Completed += OnSceneLoaded;
    }
    private void OnDisable()
    {
        m_SceneHandle.Completed -= OnSceneLoaded;
        Addressables.UnloadSceneAsync(m_SceneHandle);
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
        Addressables.ClearDependencyCacheAsync("DemoScene");//Clearing cache for downloading screen

        m_SceneHandle = Addressables.DownloadDependenciesAsync("DemoScene");
        Time.timeScale = 1f;
        
    }
    void Start()
    {        
        fullLoadingBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        LoadingText.text = m_SceneHandle.GetDownloadStatus().DownloadedBytes / 1000000 + " / " + m_SceneHandle.GetDownloadStatus().TotalBytes / 1000000 + " MB ";
        fullLoadingBar.fillAmount = m_SceneHandle.GetDownloadStatus().Percent;
        if (m_SceneHandle.IsDone && fullLoadingBar.fillAmount == 1)
        {
            GetNewScene();
        }
    }
    
   

}
