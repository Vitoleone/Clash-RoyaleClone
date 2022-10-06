using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using UnityEngine.AddressableAssets;

public class Banner : MonoBehaviour
{
    private BannerView bannerView;
    public BundleManager bundle;


    public void Start()
    {
        DontDestroyOnLoad(this);
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "LoadingScene")
        {
            bannerView.Hide();
        }
        else
        {
            bannerView.Show();
        }
        
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif



        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.TopRight);


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

#if UNITY_EDITOR
        bannerView.OnAdLoaded += OnAdsLoaded;
#endif

        // Load the banner with the request.
        bannerView.LoadAd(request);

        bannerView.OnAdLoaded += OnAdsLoaded;
    }

    private void OnAdsLoaded(object sender, EventArgs e)
    {
        
        bundle.bannerLoaded = true;
    }
    private void OnApplicationQuit()
    {
        bundle.bannerLoaded = false;
    }
}
