using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class InterstitialAds : AdsHelper
{
    InterstitialAd interstitial;
    int adReloadsLeft;
    int adReshowsLeft;

    void Start()
    {
        adReloadsLeft = RetryAmount();
        adReshowsLeft = RetryAmount();        
    }
    public void LoadInterstitialAd()
    {
        this.interstitial = new InterstitialAd(GetAdUnitId(AdType.Interstitial));
        
        this.interstitial.OnAdLoaded += OnAdLoaded;
        this.interstitial.OnAdFailedToLoad += OnAdFailedToLoad; 
        this.interstitial.OnAdFailedToShow += AdFailedToShow;  
        this.interstitial.OnAdOpening += OnAdOpening; 
        this.interstitial.OnAdClosed += OnAdClosed;

        AdRequest request = new AdRequest.Builder().AddExtra("npa", GetNPAValue().ToString()).Build();

        this.interstitial.LoadAd(request);
    }
    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    void DestroyInterstitial()
    {
        this.interstitial.Destroy();
    }

    void ResetRetries()
    {
        adReloadsLeft = RetryAmount();
        adReshowsLeft = RetryAmount();
    }
    #region HandlerMethods

    void OnAdLoaded(object sender, EventArgs args) // Called when an ad request has successfully loaded.
    { 
        Debug.Log("Interstitial AdLoaded event received");
    }

    void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) // Called when an ad request failed to load.
    {       
        Debug.Log("Interstitial failed to load: " + args.LoadAdError.GetMessage());

        // try reloading ad
        if (adReloadsLeft > 0 && !this.interstitial.IsLoaded())
        {
            adReloadsLeft--;
            LoadInterstitialAd();
        }
        // destroy ad if reloading hasnt worked
        if ((!this.interstitial.IsLoaded()) && adReloadsLeft <= 0) DestroyInterstitial();
    }
    void AdFailedToShow(object sender, AdErrorEventArgs args) // Called when an ad request failed to show.
    {       
        Debug.Log("Interstitila AdFailedToShow event received with message: " + args.AdError.GetMessage());

        //Try again to show interstitial ad
        if (this.interstitial.IsLoaded() && adReshowsLeft > 0)
        {
            adReshowsLeft--;
            ShowInterstitial();
        }
        //Destroy ad if trying again to show hasn't worked
        if ((this.interstitial.IsLoaded()) && adReshowsLeft <= 0) DestroyInterstitial();
    }
    void OnAdOpening(object sender, EventArgs args) // Called when an ad is shown. 
    {
        Debug.Log("Interstitial AdOpening event received");
        //Disable functions for the duration of the ad
        adListener.OnAdOpening();
    }
    void OnAdClosed(object sender, EventArgs args) // Called when the ad is closed.
    {        
        Debug.Log("Interstitial AdClosed event received");
        //Enable all functions      
        adListener.OnAdClosed();
        ResetRetries();
        DestroyInterstitial();
        LoadInterstitialAd();
    }
    #endregion    
}