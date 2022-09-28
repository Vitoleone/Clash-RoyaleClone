using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] GameObject enemyCastle;
    [SerializeField] GameObject allyCastle;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject gameOverScreen;
    Castle castleInstance;
    EnemyCastle enemyCastleInstance;
    void Start()
    {
        enemyCastleInstance = enemyCastle.GetComponent<EnemyCastle>();
        castleInstance = allyCastle.GetComponent<Castle>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(castleInstance.instance.health <= 0)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else if(enemyCastleInstance.instance.health <= 0)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0;
        }
        
    }
}
