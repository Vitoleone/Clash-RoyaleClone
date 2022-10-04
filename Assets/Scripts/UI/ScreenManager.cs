using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] GameObject enemyCastle;
    [SerializeField] GameObject allyCastle;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Text timerText;
    float timer = 15f;
    Castle castleInstance;
    EnemyCastle enemyCastleInstance;
    
    private void GetNewScene()
    {
        
    }
   
    void Start()
    {
        enemyCastleInstance = enemyCastle.GetComponent<EnemyCastle>();
        castleInstance = allyCastle.GetComponent<Castle>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = "Time Left: " + Mathf.FloorToInt(timer).ToString();
        if(timer > 0)
        {
            if (castleInstance.instance.health <= 0)
            {
                gameOverScreen.SetActive(true);
                Time.timeScale = 0;
            }
            else if (enemyCastleInstance.instance.health <= 0)
            {
                
                winScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else
        {
            if(castleInstance.instance.health <= enemyCastleInstance.instance.health)
            {
                gameOverScreen.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Debug.Log("EnemyHealth = " + enemyCastleInstance.instance.health + " AllyHealth: " + castleInstance.instance.health);
                winScreen.SetActive(true);
                Time.timeScale = 0;
            }

        }
      
        
    }
    public void RestartGame()
    {
        AsyncOperationHandle m_SceneHandle;
        m_SceneHandle = Addressables.DownloadDependenciesAsync("LoadingScene");
        m_SceneHandle.Completed += OnSceneLoaded;
    }
    private void OnSceneLoaded(AsyncOperationHandle obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Addressables.LoadSceneAsync("LoadingScene", LoadSceneMode.Single, true);
            obj.Completed -= OnSceneLoaded;
        }

    }
}
