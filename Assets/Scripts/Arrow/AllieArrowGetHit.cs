using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllieArrowGetHit : MonoBehaviour
{
    [SerializeField] float damage = 20f;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Castle"))
        {
            other.gameObject.GetComponent<EnemyCastle>().GetHit(damage);
            Destroy(gameObject);
        }
    }
}
