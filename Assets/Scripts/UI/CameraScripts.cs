using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    [SerializeField] GameObject leftBridge;
    [SerializeField] GameObject rightBridge;
    [SerializeField] GameObject leftCamera;
    [SerializeField] GameObject rightCamera;
    [SerializeField] GameObject castleCamera;
    [SerializeField] GameObject castle;

    int unitsInLeftBridge = 0;
    int unitsInRightBridge = 0;

    // Update is called once per frame
    void Update()
    {
        unitsInLeftBridge = leftBridge.GetComponent<GetObjectsInTheCollider>().allUnits.Count;
        unitsInRightBridge = rightBridge.GetComponent<GetObjectsInTheCollider>().allUnits.Count;
        if(unitsInLeftBridge > unitsInRightBridge && castle.GetComponent<Castle>().getHit == false)
        {
            leftCamera.SetActive(true);
            rightCamera.SetActive(false);
            castleCamera.SetActive(false);
        }
        else if(unitsInLeftBridge < unitsInRightBridge && castle.GetComponent<Castle>().getHit == false)
        {
            rightCamera.SetActive(true);
            leftCamera.SetActive(false);
            castleCamera.SetActive(false);
        }
        else
        {
            castleCamera.SetActive(true);
            rightCamera.SetActive(false);
            leftCamera.SetActive(false);
        }

        
    }
}
