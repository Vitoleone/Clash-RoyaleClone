using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnDragon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject newDragon;
    bool canSpawn = true;
    float energAmount = 3;
    [SerializeField] GameObject energyBar;
    private AsyncOperationHandle<GameObject> mGiantLoadingHandle;

    private void OnGiantInstantiated(AsyncOperationHandle<GameObject> gameObject)
    {
        newDragon = gameObject.Result;
        newDragon.GetComponent<DragonAlly>().enabled = false;
        newDragon.transform.Find("sJ001").GetComponent<SkinnedMeshRenderer>().material.DOFade(0.3f, 0);
        //newgiant.GetComponent<Renderer>().material.DOFade(0.3f, 0f);
        newDragon.GetComponent<NavMeshAgent>().enabled = false;
        newDragon.GetComponent<Animator>().enabled = false;

        RaycastAndMove();
        canSpawn = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canSpawn && energyBar.GetComponent<EnergyBar>().instance.currentEnergy >= energAmount)
        {
            energyBar.GetComponent<EnergyBar>().instance.UseEnergy(energAmount);

            mGiantLoadingHandle = Addressables.InstantiateAsync("DragonAlly", transform.position, Quaternion.identity);
            mGiantLoadingHandle.Completed += OnGiantInstantiated;


        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if ((newDragon.GetComponent<DragonAlly>().enabled == false))
        {
            RaycastAndMove();
        }
        newDragon.GetComponent<DragonAlly>().enabled = true;
        newDragon.GetComponent<NavMeshAgent>().enabled = true;
        newDragon.GetComponent<Animator>().enabled = true;
        newDragon.transform.Find("sJ001").GetComponent<SkinnedMeshRenderer>().material.DOFade(1f, 0);
        canSpawn = true;
    }



    public void OnDrag(PointerEventData eventData)
    {
       
        if ((newDragon.GetComponent<DragonAlly>().enabled == false))
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
                newDragon.transform.position = new Vector3(raycastHit.point.x, 1, raycastHit.point.z);
            }

        }
    }
}
