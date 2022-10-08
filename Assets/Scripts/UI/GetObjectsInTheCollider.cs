using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class GetObjectsInTheCollider : MonoBehaviour
{
    public List<GameObject> allUnits = new List<GameObject>();

    private void Update()
    {
        foreach (GameObject obj in allUnits)
        {
            if(obj == null)
            {
                allUnits.Remove(obj);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        allUnits.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        allUnits.Remove(other.gameObject);
    }
}
