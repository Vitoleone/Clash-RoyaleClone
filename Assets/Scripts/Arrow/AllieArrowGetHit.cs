using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllieArrowGetHit : MonoBehaviour
{
    [SerializeField] float damage = 20f;
    [SerializeField] GameObject getHitParticle;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Castle"))
        {
            other.gameObject.GetComponent<EnemyCastle>().GetHit(damage);
            Instantiate(getHitParticle,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
