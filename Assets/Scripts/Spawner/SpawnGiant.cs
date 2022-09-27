using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGiant : MonoBehaviour
{
    public GameObject giant;
    [SerializeField] GameObject SpawnPoint;
    GameObject newGiant;
    public void Spawn()
    {
        newGiant = Instantiate(giant,SpawnPoint.transform.position,Quaternion.identity);
        
    }
}
