using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
{
    [SerializeField] GameObject Castle;
    [SerializeField] GameObject healthBarBackground;
    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject Allie;
    EnemyCastle EnemyCastleInstance;
    IEnemy enemy;
    IAllie allie;
    Castle CastleInstance;
    float maxHealth;
    float currentHealth;
    Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        if (Castle != null)
        {
            if (Castle.gameObject.name == "Castle")
            {
                CastleInstance = Castle.GetComponent<Castle>();
                maxHealth = CastleInstance.instance.health;
            }
            else if (Castle.gameObject.name == "EnemyCastle")
            {
                EnemyCastleInstance = Castle.GetComponent<EnemyCastle>();
                maxHealth = EnemyCastleInstance.instance.health;
            }
        }
        else if(Enemy != null)
        {
            if (Enemy.gameObject.CompareTag("Enemy"))
            {
                enemy = Enemy.GetComponent<IEnemy>();
                maxHealth = enemy.GetHealth();
            }
        }
        else if (Allie != null)
        {
            if (Allie.gameObject.CompareTag("Allie"))
            {
                allie = Allie.GetComponent<IAllie>();
                maxHealth = allie.GetHealth();
            }
        }



        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Castle != null && Castle.gameObject.name == "Castle")
        {
            currentHealth = CastleInstance.instance.health;
            healthBar.fillAmount = currentHealth / maxHealth;
        }
        else if (Castle != null && Castle.gameObject.name == "EnemyCastle")
        {
            currentHealth = EnemyCastleInstance.instance.health;
            healthBar.fillAmount = currentHealth / maxHealth;
        }
        else if (enemy != null)
        {
            currentHealth = enemy.GetHealth();
            healthBar.fillAmount = currentHealth / maxHealth;
            healthBarBackground.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        }
        else if (allie != null)
        {
            currentHealth = allie.GetHealth();
            healthBar.fillAmount = currentHealth / maxHealth;
            healthBarBackground.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        }
        else
        {
            
            Destroy(healthBarBackground);
        }
        
    }
}
