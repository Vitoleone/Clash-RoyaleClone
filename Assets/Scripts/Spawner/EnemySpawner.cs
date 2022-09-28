using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies;
    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", 1.5f,1.5f);
    }
    private void Update()
    {
        
    }
    void SpawnRandomEnemy()
    {
        int randomNum = Random.Range(0, enemies.Count);
        GameObject newEnemy = Instantiate(enemies[randomNum], transform.position, Quaternion.identity);
    }

    
}
