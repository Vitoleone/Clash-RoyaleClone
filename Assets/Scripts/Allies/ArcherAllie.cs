using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ArcherAllie : MonoBehaviour, IAllie
{
    //Castle objects
    GameObject castle;
    EnemyCastle castleInstance;
    //Enemy Attributes
    public float health = 150f;
    float damage = 35f;
    float speed = 4.5f;
    float attackRate = 1.25f;
    bool canAttack = false;
    Animator myAnim;
    //Other components
    NavMeshAgent navMeshAgent;
    [SerializeField] GameObject Arrow;
    GameObject target;
    GameObject healthBar;
    public ArcherAllie instance;
    Tweener ShootTween;


    private void Awake()
    {
        instance = this;
        castle = GameObject.Find("EnemyCastle");
        castleInstance = castle.GetComponent<EnemyCastle>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>(); 
        //healthBar = transform.Find("HealthBackground").gameObject;
        

    }
    void Start()
    {
        
        Move(castle.transform.position);

    }
    private void LateUpdate()
    {
        attackRate -= Time.deltaTime;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        CheckRange(transform.position, 5f);

    }


    public void Attack()
    {
        if (target != null)
        {
            myAnim.SetBool("canAttack", true);
            GameObject arrow = Instantiate(Arrow, transform.position, Quaternion.identity);
            arrow.transform.DOMove(target.transform.position + Vector3.up *2 , 0.5f);
        }
        CancelInvoke();
    }
    public void Attack(Transform nearestEnemy)
    {
        transform.DOLookAt(nearestEnemy.transform.position, 0f);
        myAnim.SetBool("canAttack", true);
        GameObject arrow = Instantiate(Arrow, transform.position + Vector3.up * 3, Quaternion.identity);
        ShootTween = arrow.transform.DOMove(nearestEnemy.position + Vector3.up, 0.50f).OnComplete(delegate
        {
            ShootTween.Kill();
            
        });

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
                    else if(hitCollider != null && hitCollider.gameObject.name == "EnemyCastle")
                    {
                        enemies.Add(hitCollider.gameObject);
                    }
                    else if (hitCollider != null && hitCollider.gameObject.tag == "FlyEnemy")
                    {
                        enemies.Add(hitCollider.gameObject);
                    }

                }
                if (enemies.Count > 0)
                {
                    if(attackRate <= 0)
                    {
                        navMeshAgent.SetDestination(transform.position);
                        Attack(GetNearestEnemy(enemies));//Attacks the enemy whic is the nearest.
                        attackRate = 1.25f;
                    }
                    


                }
                else
                {
                    navMeshAgent.SetDestination(castle.transform.position);
                    myAnim.SetBool("canAttack", false);
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
