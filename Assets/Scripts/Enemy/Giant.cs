using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System.Linq;

public class Giant : MonoBehaviour,IEnemy
{
    //Castle objects
    GameObject castle;
    Castle castleInstance;
    //Enemy Attributes
    [SerializeField]float health = 300f;
    float damage = 100f;
    float speed = 3.5f;
    float attackRate = 2f;
    bool canAttack = false;
    Animator myAnim;
    //Other components
    NavMeshAgent navMeshAgent;

    private void Awake()
    {
        castle = GameObject.Find("Castle");
        castleInstance = castle.GetComponent<Castle>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();
    }
    void Start()
    {
        Move();

    }
    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        CheckRange(transform.position, 4f);
        if(canAttack)
        {
            
            InvokeRepeating("Attack",attackRate,attackRate);
        }
    }


    public void Attack()
    {
        
        if(castle != null)
        {
            myAnim.SetBool("CanHit", true);
            castleInstance.GetHit(damage);
        }
        CancelInvoke();
    }

    public void Move()
    {
        navMeshAgent.SetDestination(castle.transform.position);
        myAnim.SetBool("isWalking", true);
        navMeshAgent.speed = speed;
    }
    public void CheckRange(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        List<Collider> collidersList = hitColliders.ToList();
        foreach (var hitCollider in collidersList)
        {
            if(hitCollider != null)
            {
                if(hitCollider.gameObject.name == "Castle")
                {
                    myAnim.SetBool("isWalking", false);
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
