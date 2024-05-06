using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class BannerScript : MonoBehaviour
{
    // Sample ad unit ID for testing
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/6300978111"; // Sample ad unit ID for testing on Android
#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-3940256099942544/2934735716"; // Sample ad unit ID for testing on iOS
#else
    private string _adUnitId = "unexpected_platform";
#endif

    BannerView _bannerView;
    public GameObject promptPanel; // Reference to the UI panel for the prompt box

    public Text promptText; // Reference to the Text component in the prompt box
   // public GameObject settingsPanel;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
            CreateBannerView();
            //enableAdds()
            EnableAds();
        });
    }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAd();
        }

        AdSize adSize = AdSize.Banner;
        _bannerView = new BannerView(_adUnitId, adSize,AdPosition.Bottom);
    }

    public void LoadAd()
    {
        // create an ad request
        AdRequest adRequest = new AdRequest();

        // Add keywords
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
        ListenToAdEvents();
    }

    public void EnableAds()
    {
        if (_bannerView == null)
        {
            CreateBannerView();
            LoadAd();
        }
        else
        {
            _bannerView.Show();
        }
    }

    public void DisableAds()
    {
        if (_bannerView != null)
        {
            _bannerView.Hide();
        }
        
    }

    private void ListenToAdEvents()
    {
        // Subscribe to banner ad events here.
    }
    public void ShowPromptBox()
    {
        _bannerView.Hide();
        promptPanel.SetActive(true); // Show the prompt panel
        promptText.text = "  Upgrade for an\n   ad-free game!  \n\n   Pay now ";

    }

    // Method to hide the prompt box
    public void HidePromptBox()
    {
        promptPanel.SetActive(false); // Hide the prompt panel
        _bannerView.Show();
    }
    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }
    public void ShowSettingsPanel()
    {
       // settingsPanel.SetActive(true);
        
    }
}
