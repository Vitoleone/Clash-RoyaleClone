using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonbalGetHit : MonoBehaviour
{
    [SerializeField] float damage = 20f;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Allie"))
        {
            other.gameObject.GetComponent<IAllie>().GetHit(damage);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
