using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class BannerAds : AdsHelper
{
    BannerView bannerAd;
    int adReloadsLeft;

    void Start()
    {   
        adReloadsLeft = RetryAmount();
    }
    public void LoadBannerAd()
    {
        // Create an adaptive banner at the BOTTOM of the screen.
        this.bannerAd = new BannerView(GetAdUnitId(AdType.Banner), AdSize.GetPortraitAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth), AdPosition.Top);

        this.bannerAd.OnAdLoaded += this.OnAdLoaded;        
        this.bannerAd.OnAdFailedToLoad += this.OnAdFailedToLoad;        
        this.bannerAd.OnAdOpening += this.OnAdOpened;        
        this.bannerAd.OnAdClosed += this.OnAdClosed;

        AdRequest request = new AdRequest.Builder().AddExtra("npa", GetNPAValue().ToString()).Build();         

        this.bannerAd.LoadAd(request);
        
        this.bannerAd.Hide(); //Hide koska muuten se lataamisen lisäks näyttää sen mainoksen
    }

    public void ShowBannerAd()
    {
        this.bannerAd.Show();
    }

    public void DestroyBanner()
    {
        this.bannerAd.Destroy();
    }   

    #region HandlerMethods
    void OnAdLoaded(object sender, EventArgs args) // Called when an ad request has successfully loaded.
    {        
        Debug.Log("BannerAdLoaded event received");
    }
    void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) // Called when an ad request failed to load.
    {        
        Debug.Log("FailedToReceiveBannerAd event received with message: " + args.LoadAdError.GetMessage());
        if (adReloadsLeft > 0)
        {
            adReloadsLeft--;
            LoadBannerAd();
        }    
        if (adReloadsLeft <= 0)
        {
            DestroyBanner();
        }
    }
    void OnAdOpened(object sender, EventArgs args) // Called when an ad is clicked.
    {  
        Debug.Log("BannerOnAdOpened event received");
        //"If you're using an analytics package to track clickthroughs, this is a good place to record one"
        //Disable functions for the duration of the ad
        adListener.OnAdOpening();
    }
    void OnAdClosed(object sender, EventArgs args) // Called when the user returned to the app after an ad click.
    {        
        Debug.Log("BannerOnAdClosed event received");
        //Enable all functions
        adListener.OnAdClosed();
    }
    #endregion   
}
