using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : MonoBehaviour, IEnemy
{
    //Castle objects
    GameObject castle;
    Castle castleInstance;
    //Enemy Attributes
    [SerializeField] float health = 180f;
    float damage = 50;
    float speed = 3.5f;
    float attackRate = 2f;
    float attackRange = 5f;
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
        Move(castle.transform.position);

    }
    private void Update()
    {
        attackRate -= Time.deltaTime;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        CheckRange(transform.position, attackRange);
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
        if (nearestEnemy.gameObject.name == "Castle")
        {
            transform.DOLookAt(castle.transform.position, 0f);
            castleInstance.instance.GetHit(damage);
        }
        else if (nearestEnemy.gameObject.CompareTag("Allie"))
        {
            nearestEnemy.gameObject.GetComponent<IAllie>().GetHit(damage);
            transform.DOLookAt(nearestEnemy.transform.position,0f);
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
                    if (hitCollider != null && hitCollider.gameObject.tag == "Allie")
                    {
                        enemies.Add(hitCollider.gameObject);
                    }
                    else if (hitCollider != null && hitCollider.gameObject.name == "Castle")
                    {
                        enemies.Add(hitCollider.gameObject);
                    }
                    else if (hitCollider != null && hitCollider.gameObject.name == "FlyAlly")
                    {
                        enemies.Add(hitCollider.gameObject);
                    }

                }
                if (enemies.Count > 0)
                {

                    if (attackRate <= 0)
                    {
                        navMeshAgent.SetDestination(transform.position);
                        Attack(GetNearestEnemy(enemies));//Attacks the enemy whic is the nearest.
                        attackRate = 2f;
                    }
                    


                }
                else
                {
                    attackRate = 2f;
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
