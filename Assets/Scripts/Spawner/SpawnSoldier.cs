using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnSoldier : MonoBehaviour
{
    public GameObject soldier;
    public GameObject newSoldier;
    [SerializeField] Material soldierMaterial;
    bool canSpawn = true;
    private Vector3 mOffset;
    private float mZCoord;
    float energAmount = 2;
    [SerializeField] GameObject energyBar;

    private void OnMouseDown()
    {
        if (canSpawn && energyBar.GetComponent<EnergyBar>().instance.currentEnergy >= energAmount)
        {
            energyBar.GetComponent<EnergyBar>().instance.UseEnergy(energAmount);
            newSoldier = Instantiate(soldier, transform.position, Quaternion.identity);
            ComponentAdjustment(false);
            soldierMaterial.DOFade(0.3f, 0);
            canSpawn = false;
        }
        mZCoord = Camera.main.WorldToScreenPoint(newSoldier.transform.position).z;
        mOffset = newSoldier.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void OnMouseDrag()
    {
        //z= 10.70939, x= -0.2096049
        if ((GetMouseWorldPos() + mOffset).z <= -8f)
        {
            newSoldier.transform.position = GetMouseWorldPos() + mOffset;
        }

    }
    private void OnMouseUp()
    {
        if ((GetMouseWorldPos() + mOffset).z <= -8f)
        {
            newSoldier.transform.position = GetMouseWorldPos() + mOffset;
        }
        ComponentAdjustment(true);
        soldierMaterial.DOFade(1f, 0);
        newSoldier = null;
        canSpawn = true;
    }

    void ComponentAdjustment(bool value)
    {
        newSoldier.GetComponentsInChildren<SoldiersAllie>()[0].enabled = value;
        newSoldier.GetComponentsInChildren<SoldiersAllie>()[1].enabled = value;
        newSoldier.GetComponentsInChildren<SoldiersAllie>()[2].enabled = value;

        newSoldier.GetComponentsInChildren<NavMeshAgent>()[0].enabled = value;
        newSoldier.GetComponentsInChildren<NavMeshAgent>()[1].enabled = value;
        newSoldier.GetComponentsInChildren<NavMeshAgent>()[2].enabled = value;

        newSoldier.GetComponentsInChildren<Animator>()[0].enabled = value;
        newSoldier.GetComponentsInChildren<Animator>()[1].enabled = value;
        newSoldier.GetComponentsInChildren<Animator>()[2].enabled = value;

    }
}
