using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallGetHit : MonoBehaviour
{
    [SerializeField] float damage = 20f;
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("FlyEnemy"))
        {
            other.gameObject.GetComponent<IEnemy>().GetHit(damage);
            Destroy(gameObject); 
        }
        else if(other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
