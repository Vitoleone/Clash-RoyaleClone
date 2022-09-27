using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Archer : MonoBehaviour,IEnemy
{
    //Castle objects
    GameObject castle;
    Castle castleInstance;
    //Enemy Attributes
    [SerializeField] float health = 150f;
    float damage = 35f;
    float speed = 4.5f;
    float attackRate = 1.25f;
    bool canAttack = false;
    //Other components
    NavMeshAgent navMeshAgent;
    [SerializeField]GameObject Arrow;

    private void Awake()
    {
        castle = GameObject.Find("Castle");
        castleInstance = castle.GetComponent<Castle>();
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        CheckRange(transform.position, 5f);
        if (canAttack)
        {
            InvokeRepeating("Attack", attackRate, attackRate);
        }
    }


    public void Attack()
    {
        if (castle != null)
        {
            GameObject arrow = Instantiate(Arrow, transform.position, Quaternion.identity);
            arrow.transform.DOMove(castle.transform.position, 0.5f);
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
                    canAttack = true;
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
