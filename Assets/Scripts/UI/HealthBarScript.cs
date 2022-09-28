using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
{
    [SerializeField] GameObject Castle;
    [SerializeField] GameObject healthBarBackground;
    EnemyCastle EnemyCastleInstance;
    Castle CastleInstance;
    float maxHealth;
    float currentHealth;
    Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        if(Castle.gameObject.name == "Castle")
        {
            CastleInstance = Castle.GetComponent<Castle>();
            maxHealth = CastleInstance.instance.health;
        }
        else if(Castle.gameObject.name == "EnemyCastle")
        {
            EnemyCastleInstance = Castle.GetComponent<EnemyCastle>();
            maxHealth = EnemyCastleInstance.instance.health;
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
        else
        {
            Destroy(healthBarBackground);
        }
        
    }
}
