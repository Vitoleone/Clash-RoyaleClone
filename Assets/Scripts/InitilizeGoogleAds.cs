using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitilizeGoogleAds : MonoBehaviour
{
    [SerializeField]BundleManager bundle;
    void Start()
    {
        MobileAds.Initialize(initStatus => { bundle.bannerLoaded = true; });
    }

    
   
}
