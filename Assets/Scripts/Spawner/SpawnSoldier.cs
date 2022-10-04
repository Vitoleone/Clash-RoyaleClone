using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;

public class SpawnSoldier : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject soldier;
    public GameObject newSoldier;
    [SerializeField] Material soldierMaterial;
    [SerializeField] Material soldierGhostMaterial;
    bool canSpawn = true;
    float energAmount = 2;
    [SerializeField] GameObject energyBar;
    private AsyncOperationHandle<GameObject> mSoldierHandle;

 
    private void OnSoldierInstantiated(AsyncOperationHandle<GameObject> gameObject)
    {
        if (gameObject.Status == AsyncOperationStatus.Succeeded)
        {
            newSoldier = gameObject.Result;
            canSpawn = false;
            RaycastAndMove();
            ComponentAdjustment(false);
        }
    }

    void ComponentAdjustment(bool value)
    {

        if(newSoldier != null)
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
            if(value == false)
            {
                foreach (MeshRenderer renderer in newSoldier.GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.material = soldierGhostMaterial;
                }
                foreach (SkinnedMeshRenderer renderer in newSoldier.GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    renderer.material = soldierGhostMaterial;
                }
            }
            else
            {
                foreach (MeshRenderer renderer in newSoldier.GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.material = soldierMaterial;
                }
                foreach (SkinnedMeshRenderer renderer in newSoldier.GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    renderer.material = soldierMaterial;
                }
            }
           
        }
        

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canSpawn && energyBar.GetComponent<EnergyBar>().instance.currentEnergy >= energAmount)
        {
            energyBar.GetComponent<EnergyBar>().instance.UseEnergy(energAmount);
            mSoldierHandle = Addressables.InstantiateAsync("SoldiersAllie", transform.position, Quaternion.identity);
            mSoldierHandle.Completed += OnSoldierInstantiated;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if ( newSoldier != null && newSoldier.GetComponentsInChildren<SoldiersAllie>()[0].enabled == false)
        {
            RaycastAndMove();
            newSoldier.GetComponentInChildren<SkinnedMeshRenderer>().material = soldierMaterial;
        }
        ComponentAdjustment(true);
        canSpawn = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //z= 10.70939, x= -0.2096049
        if (newSoldier == mSoldierHandle.Result && newSoldier.GetComponentsInChildren<SoldiersAllie>()[0].enabled == false)
        {

            RaycastAndMove();
        }
    }
    void RaycastAndMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.collider.CompareTag("PlayerField"))
            {
                newSoldier.transform.position = new Vector3(raycastHit.point.x, 1, raycastHit.point.z);
            }

        }
    }
}
