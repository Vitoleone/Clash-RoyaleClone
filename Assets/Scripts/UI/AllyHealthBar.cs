using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyHealthBar : MonoBehaviour
{
    [SerializeField] GameObject Ally;
    [SerializeField] GameObject healthBarBackground;
    ArcherAllie ArcherInstance;
    float maxHealth;
    float currentHealth;
    Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        if (Ally.gameObject.CompareTag("Allie"))
        {
            ArcherInstance = Ally.GetComponent<ArcherAllie>();
            maxHealth = ArcherInstance.instance.health;
            

        }

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Ally != null && Ally.gameObject.name == "AllieArcher")
        {
            currentHealth = ArcherInstance.instance.health;
            healthBar.fillAmount = currentHealth / maxHealth;
        }
        else
        {
            Destroy(healthBarBackground);
        }

    }
}
