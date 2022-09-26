using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies;
    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", 2f,2f);
    }
    private void Update()
    {
        
    }
    void SpawnRandomEnemy()
    {
        int randomNum = Random.Range(0, enemies.Count);
        GameObject newEnemy = Instantiate(enemies[randomNum], Random.insideUnitCircle * 4f, Quaternion.identity);
    }

    
}
