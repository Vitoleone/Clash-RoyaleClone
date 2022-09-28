using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    float maxEnergy = 10;
    public float currentEnergy = 0;
    public EnergyBar instance;
    Image HealthBar;
    private void Awake()
    {
        HealthBar = GetComponent<Image>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnergy >= 0 && currentEnergy <= 10)
        {
            currentEnergy += Time.deltaTime;
            HealthBar.fillAmount = currentEnergy / maxEnergy;
        }
        
    }
    public void UseEnergy(float amount)
    {
        if(currentEnergy - amount <= 0)
        {
            currentEnergy = 0;
        }
        else
        {
            currentEnergy -= amount;
        }
    }
}
