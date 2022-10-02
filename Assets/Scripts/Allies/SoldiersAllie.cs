using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SoldiersAllie : MonoBehaviour,IAllie
{
    //Castle objects
    GameObject castle;
    EnemyCastle castleInstance;
    //Enemy Attributes
    public float health = 50f;
    float damage = 20f;
    float speed = 5f;
    float attackRate = 1.25f;
    bool canAttack = false;
    Animator myAnim;
    SoldiersAllie instance;
    //Other components
    Tweener moveTween;
    Tweener rotateTween;
    NavMeshAgent navMeshAgent;

    private void Awake()
    {
        castle = GameObject.Find("EnemyCastle");
        castleInstance = castle.GetComponent<EnemyCastle>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();
        instance = this;
    }
    void Start()
    {
        Move(castle.transform.position);

    }
    private void Update()
    {
        attackRate -= Time.deltaTime;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        CheckRange(transform.position, 2.5f);
    }


    public void Attack()
    {
        if (castle != null)
        {
            myAnim.SetBool("CanAttack", true);
            castleInstance.GetHit(damage);
        }
        CancelInvoke();
    }
    public void Attack(Transform nearestEnemy)
    {

        myAnim.SetBool("CanAttack", true);
        if (nearestEnemy.gameObject.name == "EnemyCastle")
        {
            castleInstance.instance.GetHit(damage);
        }
        else if (nearestEnemy.gameObject.CompareTag("Enemy"))
        {
            nearestEnemy.gameObject.GetComponent<IEnemy>().GetHit(damage);
        }

    }
    public void Move(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
        navMeshAgent.speed = speed;
    }
    public void CheckRange(Vector3 center, float radius)
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
                    if (hitCollider != null && hitCollider.gameObject.tag == "Enemy")
                    {
                        enemies.Add(hitCollider.gameObject);
                    }
                    else if (hitCollider != null && hitCollider.gameObject.name == "EnemyCastle")
                    {
                        enemies.Add(hitCollider.gameObject);
                    }

                }
                if (enemies.Count > 0)
                {
                    navMeshAgent.isStopped = true;
                    if (attackRate <= 0)
                    {
                        
                        Attack(GetNearestEnemy(enemies));//Attacks the enemy whic is the nearest.
                        attackRate = 2f;
                    }
                    


                }
                else
                {
                    attackRate = 2f;
                    navMeshAgent.isStopped = false;
                    myAnim.SetBool("CanAttack", false);
                    navMeshAgent.SetDestination(castle.transform.position);
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
    public void GetHit(float damage)
    {
        health -= damage;
    }
    public float GetHealth()
    {
        return health;
    }
}
