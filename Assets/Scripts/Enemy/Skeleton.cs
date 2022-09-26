using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour,IEnemy
{
    //Castle objects
    GameObject castle;
    Castle castleInstance;
    //Enemy Attributes
    public float health = 50f;
    float damage = 20f;
    float speed = 5f;
    float attackRate = 1.25f;
    Skeleton instance;
    //Other components
    Tweener moveTween;
    Tweener rotateTween;
    NavMeshAgent navMeshAgent;

    private void Awake()
    {
        castle = GameObject.Find("Castle");
        castleInstance = castle.GetComponent<Castle>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        instance = this;
    }
    void Start()
    {
        Move();

    }
    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        CheckRange(transform.position, 1f);
        if (navMeshAgent.velocity == Vector3.zero)
        {
            InvokeRepeating("Attack", attackRate, attackRate);
            Debug.Log("Attacked");
        }
    }


    public void Attack()
    {
        if (castle != null)
        {
            castleInstance.GetHit(damage);
        }
        CancelInvoke();
    }

    public void Move()
    {
        navMeshAgent.SetDestination(castle.transform.position);
        navMeshAgent.speed = speed;
    }
    public void CheckRange(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        List<Collider> collidersList = hitColliders.ToList();
        foreach (var hitCollider in collidersList)
        {
            if (hitCollider != null)
            {
                if (hitCollider.gameObject.name == "Castle")
                {
                    navMeshAgent.velocity = Vector3.zero;
                }
            }
            else
            {
                collidersList.Remove(hitCollider);
            }
        }
    }
    public void GetHit(float damage)
    {
        health -= damage;
    }


}
