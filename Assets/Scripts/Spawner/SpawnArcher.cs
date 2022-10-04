using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.EventSystems;

public class SpawnArcher : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject archer;
    public GameObject newArcher;
    bool canSpawn = true;
    private Vector3 mOffset;
    private float mZCoord;
    float energAmount = 2;
    [SerializeField] GameObject energyBar;
    private AsyncOperationHandle<GameObject> mArcherLoadingHandle;

    private void OnArcherInstantiated(AsyncOperationHandle<GameObject> gameObject )
    {
        if(gameObject.Status == AsyncOperationStatus.Succeeded)
        {
            newArcher = gameObject.Result;
            newArcher.GetComponent<ArcherAllie>().enabled = false;
            newArcher.transform.Find("U3DMesh").GetComponent<SkinnedMeshRenderer>().materials[0].DOFade(0.3f, 0);
            newArcher.transform.Find("U3DMesh").GetComponent<SkinnedMeshRenderer>().materials[1].DOFade(0.3f, 0);
            newArcher.transform.Find("U3DMesh").GetComponent<SkinnedMeshRenderer>().materials[2].DOFade(0.3f, 0);
            RaycastAndMove();
            newArcher.GetComponent<NavMeshAgent>().enabled = false;
            newArcher.GetComponent<Animator>().enabled = false;
           
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //z= 10.70939, x= -0.2096049
        if ( newArcher.GetComponent<ArcherAllie>().enabled == false)
        {
            RaycastAndMove();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if ( newArcher.GetComponent<ArcherAllie>().enabled == false)
        {
            RaycastAndMove();
        }
        newArcher.GetComponent<ArcherAllie>().enabled = true;
        newArcher.GetComponent<NavMeshAgent>().enabled = true;
        newArcher.GetComponent<Animator>().enabled = true;
        newArcher.transform.Find("U3DMesh").GetComponent<SkinnedMeshRenderer>().materials[0].DOFade(1, 0);
        newArcher.transform.Find("U3DMesh").GetComponent<SkinnedMeshRenderer>().materials[1].DOFade(1, 0);
        newArcher.transform.Find("U3DMesh").GetComponent<SkinnedMeshRenderer>().materials[2].DOFade(1, 0);
        canSpawn = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canSpawn && energyBar.GetComponent<EnergyBar>().instance.currentEnergy >= energAmount)
        {
            energyBar.GetComponent<EnergyBar>().instance.UseEnergy(energAmount);
            //newArcher = Instantiate(archer, transform.position,Quaternion.identity);
            mArcherLoadingHandle = Addressables.InstantiateAsync("AllieArcher", transform.position, Quaternion.identity);
            mArcherLoadingHandle.Completed += OnArcherInstantiated;


            canSpawn = false;
        }
    }
    void RaycastAndMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.collider.CompareTag("PlayerField"))
            {
                newArcher.transform.position = new Vector3(raycastHit.point.x, 1, raycastHit.point.z);
            }

        }
    }
}
