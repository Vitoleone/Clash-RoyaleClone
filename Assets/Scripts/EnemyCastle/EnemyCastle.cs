using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCastle : MonoBehaviour
{
    public float health = 3000;
    float attackRate = 1.25f;
    public EnemyCastle instance;
    Tweener ShootTween;
    [SerializeField] GameObject canonBall;
    [SerializeField] GameObject getHitParticle;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        attackRate -= Time.deltaTime;
        if (health <= 0)
        {
            GetDestroyed();
        }
        CheckRangeAndAttackNearest(transform.position, 7.5f);


    }
    public void GetHit(float damage)
    {
        health -= damage;
        Instantiate(getHitParticle, transform.position + Vector3.up * 1.5f, Quaternion.identity);
    }

    public void Attack(Transform nearestEnemy)
    {

        GameObject canon = Instantiate(canonBall, transform.position + Vector3.up * 3, Quaternion.identity);
        ShootTween = canon.transform.DOMove(nearestEnemy.position, 0.35f).OnComplete(delegate
        {
            ShootTween.Kill();
        });

    }
    public void GetDestroyed()
    {
        Destroy(gameObject);
    }
    public void CheckRangeAndAttackNearest(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        if (hitColliders.Length > 0)
        {
            List<Collider> collidersList = hitColliders.ToList();

            List<GameObject> enemies = new List<GameObject>();
            if (collidersList.Count > 0)
            {
                foreach (var hitCollider in collidersList)
                {
                    if (hitCollider != null && hitCollider.gameObject.tag == "Allie")
                    {
                        enemies.Add(hitCollider.gameObject);
                    }
                }
                if (enemies.Count > 0 && attackRate <= 0)
                {
                    Attack(GetNearestEnemy(enemies));//Attacks the enemy whic is the nearest.
                    attackRate = 1.25f;

                }
                else
                {

                }

            }
        }

    }

    Transform GetNearestEnemy(List<GameObject> enemies)
    {
        Transform nearestEnemy;
        List<float> distances = new List<float>();

        for (int i = 0; i < enemies.Count; i++)
        {
            distances.Add(Vector3.Distance(gameObject.transform.position, enemies[i].transform.position));
        }
        int index = distances.FindIndex(distance => distances.Min() == distance);// used Linq for getting the min distance value from distances list.
        nearestEnemy = enemies[index].transform;
        return nearestEnemy;
    }
}
