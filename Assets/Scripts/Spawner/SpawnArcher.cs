using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnArcher : MonoBehaviour
{
    public GameObject archer;
    public GameObject newArcher;
    bool canSpawn = true;
    private Vector3 mOffset;
    private float mZCoord;
    float energAmount = 2;
    [SerializeField] GameObject energyBar;
    private AsyncOperationHandle<GameObject> mArcherLoadingHandle;

    private void OnMouseDown()
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
    private void OnArcherInstantiated(AsyncOperationHandle<GameObject> gameObject )
    {
        if(gameObject.Status == AsyncOperationStatus.Succeeded)
        {
            newArcher = gameObject.Result;
            newArcher.GetComponent<ArcherAllie>().enabled = false;
            newArcher.transform.Find("U3DMesh").GetComponent<SkinnedMeshRenderer>().materials[0].DOFade(0.3f, 0);
            newArcher.transform.Find("U3DMesh").GetComponent<SkinnedMeshRenderer>().materials[1].DOFade(0.3f, 0);
            newArcher.transform.Find("U3DMesh").GetComponent<SkinnedMeshRenderer>().materials[2].DOFade(0.3f, 0);
            //newgiant.GetComponent<Renderer>().material.DOFade(0.3f, 0f);
            newArcher.GetComponent<NavMeshAgent>().enabled = false;
            newArcher.GetComponent<Animator>().enabled = false;
            mZCoord = Camera.main.WorldToScreenPoint(newArcher.transform.position).z;
            mOffset = newArcher.transform.position - GetMouseWorldPos();
        }
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
        if ((GetMouseWorldPos() + mOffset).z <= -8f && newArcher.GetComponent<ArcherAllie>().enabled == false)
        {
            newArcher.transform.position = GetMouseWorldPos() + mOffset;
        }

    }
    private void OnMouseUp()
    {
        if ((GetMouseWorldPos() + mOffset).z <= -8f && newArcher.GetComponent<ArcherAllie>().enabled == false)
        {
            newArcher.transform.position = GetMouseWorldPos() + mOffset;
        }
        newArcher.GetComponent<ArcherAllie>().enabled = true;
        newArcher.GetComponent<NavMeshAgent>().enabled = true;
        newArcher.GetComponent<Animator>().enabled = true;
        newArcher.transform.Find("U3DMesh").GetComponent<SkinnedMeshRenderer>().materials[0].DOFade(1, 0);
        newArcher.transform.Find("U3DMesh").GetComponent<SkinnedMeshRenderer>().materials[1].DOFade(1, 0);
        newArcher.transform.Find("U3DMesh").GetComponent<SkinnedMeshRenderer>().materials[2].DOFade(1, 0);
        canSpawn = true;
    }
}
