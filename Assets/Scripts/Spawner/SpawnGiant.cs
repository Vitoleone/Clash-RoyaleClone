using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class SpawnGiant : MonoBehaviour
{
    public GameObject giant;
    public GameObject newgiant;
    bool canSpawn = true;
    private Vector3 mOffset;
    private float mZCoord;

    private void OnMouseDown()
    {
        if(canSpawn)
        {
            newgiant = Instantiate(giant,transform);
            newgiant.GetComponent<GiantAllie>().enabled = false;
            newgiant.transform.Find("RockGolemMesh").GetComponent<SkinnedMeshRenderer>().material.DOFade(0.3f,0);
            //newgiant.GetComponent<Renderer>().material.DOFade(0.3f, 0f);
            newgiant.GetComponent<NavMeshAgent>().enabled = false;
            newgiant.GetComponent<Animator>().enabled = false;
            canSpawn = false;
        }
        mZCoord = Camera.main.WorldToScreenPoint(newgiant.transform.position).z;
        mOffset = newgiant.transform.position - GetMouseWorldPos();
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
        if ( (GetMouseWorldPos() + mOffset).z <= -8f)
        {
            newgiant.transform.position = GetMouseWorldPos() + mOffset;
        }
        
    }
    private void OnMouseUp()
    {
        if ((GetMouseWorldPos() + mOffset).z <= -8f)
        {
            newgiant.transform.position = GetMouseWorldPos() + mOffset;
        }
        newgiant.GetComponent<GiantAllie>().enabled = true;
        newgiant.GetComponent<NavMeshAgent>().enabled = true;
        newgiant.GetComponent<Animator>().enabled = true;
        newgiant.transform.Find("RockGolemMesh").GetComponent<SkinnedMeshRenderer>().material.DOFade(1f, 0);
        canSpawn = true;
    }

}
