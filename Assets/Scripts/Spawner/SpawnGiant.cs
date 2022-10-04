using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

public class SpawnGiant : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    [SerializeField] GameObject deneme;

    public GameObject giant;
    public GameObject newgiant;
    bool canSpawn = true;
    private Vector3 mOffset;
    private float mZCoord;
    float energAmount = 4;
    [SerializeField]GameObject energyBar;
    private AsyncOperationHandle<GameObject> mGiantLoadingHandle;

    private void OnGiantInstantiated(AsyncOperationHandle<GameObject> gameObject)
    {
        newgiant = gameObject.Result;
        newgiant.GetComponent<GiantAllie>().enabled = false;
        newgiant.transform.Find("RockGolemMesh").GetComponent<SkinnedMeshRenderer>().material.DOFade(0.3f, 0);
        //newgiant.GetComponent<Renderer>().material.DOFade(0.3f, 0f);
        newgiant.GetComponent<NavMeshAgent>().enabled = false;
        newgiant.GetComponent<Animator>().enabled = false;

        RaycastAndMove();
        canSpawn = false;
    }

    
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
  
  
    public void OnPointerDown(PointerEventData eventData)
    {
        if (canSpawn && energyBar.GetComponent<EnergyBar>().instance.currentEnergy >= energAmount)
        {
            energyBar.GetComponent<EnergyBar>().instance.UseEnergy(energAmount);

            mGiantLoadingHandle = Addressables.InstantiateAsync("GiantAllie", transform.position, Quaternion.identity);
            mGiantLoadingHandle.Completed += OnGiantInstantiated;


        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if ((newgiant.GetComponent<GiantAllie>().enabled == false))
        {
            RaycastAndMove();
        }
        newgiant.GetComponent<GiantAllie>().enabled = true;
        newgiant.GetComponent<NavMeshAgent>().enabled = true;
        newgiant.GetComponent<Animator>().enabled = true;
        newgiant.transform.Find("RockGolemMesh").GetComponent<SkinnedMeshRenderer>().material.DOFade(1f, 0);
        canSpawn = true;
    }

   

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(GetMouseWorldPos() + mOffset);
        if ((newgiant.GetComponent<GiantAllie>().enabled == false))
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
                newgiant.transform.position = new Vector3(raycastHit.point.x, 1, raycastHit.point.z);
            }

        }
    }
}
