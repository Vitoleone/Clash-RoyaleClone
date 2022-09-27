using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject giant;
    public GameObject archer;
    public GameObject soldier;
    [SerializeField] GameObject SpawnPoint;
    GameObject newGiant;
    GameObject newSoldier;
    GameObject newArcher;
    public void SpawnGiant()
    {
        newGiant = Instantiate(giant, SpawnPoint.transform.position, Quaternion.identity);
    }
    public void SpawnSoldier()
    {
        newSoldier = Instantiate(soldier, SpawnPoint.transform.position, Quaternion.identity);
    }
    public void SpawnArcher()
    {
        newArcher = Instantiate(archer, SpawnPoint.transform.position, Quaternion.identity);
    }

}
