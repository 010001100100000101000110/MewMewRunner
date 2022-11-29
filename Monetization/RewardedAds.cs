using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class RewardedAds : AdsHelper
{
    CrackerInventory crackerInventory;
    LimitAdsPerDay limitAdsPerDay;
    RewardedAd crackerAd;
    RewardedAd resurrectionAd;

    int crackerAdReloadsLeft;
    int crackerAdReshowsLeft;

    int resAdReloadsLeft;
    int resAdReshowsLeft;

    public enum RewardAdType
    {
        Cracker,
        Resurrection
    }
    void Start()
    {
        ResetRetries(RewardAdType.Cracker);
        ResetRetries(RewardAdType.Resurrection);

        crackerInventory = FindObjectOfType<CrackerInventory>();
        limitAdsPerDay = FindObjectOfType<LimitAdsPerDay>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowRewardedAd(RewardAdType.Cracker);
        }
    }

    public void LoadRewardedAd(RewardAdType type)
    {
        if (type == RewardAdType.Cracker) crackerAd = CreateRewardedAd(type);
        if (type == RewardAdType.Resurrection) resurrectionAd = CreateRewardedAd(type);
    }

    RewardedAd CreateRewardedAd(RewardAdType type)
    {
        string adUnitId = null;
        if (type == RewardAdType.Cracker) adUnitId = GetAdUnitId(AdType.CrackerReward);
        if (type == RewardAdType.Resurrection) adUnitId = GetAdUnitId(AdType.ResurrectionReward);

        RewardedAd rewardedAd = new RewardedAd(adUnitId);

        rewardedAd.OnAdLoaded += AdLoaded;
        rewardedAd.OnAdFailedToLoad += AdFailedToLoad;
        rewardedAd.OnAdOpening += AdOpening;
        rewardedAd.OnAdFailedToShow += AdFailedToShow;
        if (type == RewardAdType.Cracker)
        {
            rewardedAd.OnAdClosed += CrackerAdClosed;
            rewardedAd.OnUserEarnedReward += UserEarnedCrackerReward;
        }
        if (type == RewardAdType.Resurrection)
        {
            rewardedAd.OnAdClosed += ResAdClosed;
            rewardedAd.OnUserEarnedReward += UserEarnedResurrectReward;
        }

        AdRequest request = new AdRequest.Builder().AddExtra("npa", GetNPAValue().ToString()).Build();
        rewardedAd.LoadAd(request);
        return rewardedAd;
    }
    public void ShowRewardedAd(RewardAdType type)
    {
        if (type == RewardAdType.Cracker) if (crackerAd.IsLoaded()) crackerAd.Show();
        if (type == RewardAdType.Resurrection) if (resurrectionAd.IsLoaded()) resurrectionAd.Show();
    }
    void ResetRetries(RewardAdType type)
    {
        if (type == RewardAdType.Cracker)
        {
            crackerAdReloadsLeft = RetryAmount();
            crackerAdReshowsLeft = RetryAmount();
        }
        if (type == RewardAdType.Resurrection)
        {
            resAdReloadsLeft = RetryAmount();
            resAdReshowsLeft = RetryAmount();
        }
    }
    public void DestroyRewardedAd(RewardAdType type)
    {
        RewardedAd rewarded = null;

        if (type == RewardAdType.Cracker) rewarded = crackerAd;
        if (type == RewardAdType.Resurrection) rewarded = resurrectionAd;
        
        rewarded.Destroy();
    }

    #region HandlerMethods
    void AdLoaded(object sender, EventArgs args) // Called when an ad request has successfully loaded.
    {
        Debug.Log("RewardedAdLoaded event received");
    }

    void AdFailedToLoad(object sender, AdFailedToLoadEventArgs args) // Called when an ad request failed to load.
    {
        Debug.Log("AdFailedToLoad event received with message: " + args.LoadAdError.GetMessage());

        //Try reloading cracker ad
        if ((!crackerAd.IsLoaded()) && crackerAdReloadsLeft > 0)
        {
            crackerAdReloadsLeft--;
            LoadRewardedAd(RewardAdType.Cracker);
        }
        //Destroy cracker ad if reloading hasn't worked
        if ((!crackerAd.IsLoaded()) && crackerAdReloadsLeft <= 0) DestroyRewardedAd(RewardAdType.Cracker);

        //Try reloading resurrection ad
        if ((!resurrectionAd.IsLoaded()) && resAdReloadsLeft > 0)
        {
            resAdReloadsLeft--;
            LoadRewardedAd(RewardAdType.Resurrection);
        }
        //Destroy resurrection ad if reloading hasn't worked
        if ((!resurrectionAd.IsLoaded()) && resAdReloadsLeft <= 0) DestroyRewardedAd(RewardAdType.Resurrection);
    }

    void AdOpening(object sender, EventArgs args) // Called when an ad is shown.
    {
        Debug.Log("RewardedAdOpening event received");
        //Disable functions for the duration of the ad
        adListener.OnAdOpening();
    }

    void AdFailedToShow(object sender, AdErrorEventArgs args) // Called when an ad request failed to show.
    {
        Debug.Log("AdFailedToShow event received with message: " + args.AdError.GetMessage());

        //Try again to show cracker ad
        if (crackerAd.IsLoaded() && crackerAdReshowsLeft > 0)
        {
            crackerAdReshowsLeft--;
            this.crackerAd.Show();
        }
        //Destroy ad if trying again to show hasn't worked
        if (crackerAd.IsLoaded() && crackerAdReshowsLeft <= 0) DestroyRewardedAd(RewardAdType.Cracker);        

        //Try again to show resurrection ad
        if (resurrectionAd.IsLoaded() && resAdReshowsLeft > 0)
        {
            resAdReshowsLeft--;
            this.resurrectionAd.Show();
        }
        //Destroy ad if trying again to show hasn't worked
        if (resurrectionAd.IsLoaded() && resAdReshowsLeft <= 0) DestroyRewardedAd(RewardAdType.Resurrection);
    }

    void CrackerAdClosed(object sender, EventArgs args) // Called when the ad is closed. KESKEN?!?!?!?!?!?
    {
        Debug.Log("RewardedAdClosed event received");
        //Enable all functions              
        adListener.OnAdClosed();
        ResetRetries(RewardAdType.Cracker);
        DestroyRewardedAd(RewardAdType.Cracker);
        LoadRewardedAd(RewardAdType.Cracker);
    }
    void ResAdClosed(object sender, EventArgs args)
    {
        Debug.Log("RewardedAdClosed event received");
        //Enable all functions  
        adListener.OnAdClosed();
        ResetRetries(RewardAdType.Resurrection);
        DestroyRewardedAd(RewardAdType.Resurrection);
        LoadRewardedAd(RewardAdType.Resurrection);
    }

    void UserEarnedCrackerReward(object sender, Reward args) // Called when the user should be rewarded for interacting with the ad.
    { 
        crackerInventory.AddToCrackerCount(1);
        limitAdsPerDay.OnUserWatchedDailyRewardAd();      
    }
    void UserEarnedResurrectReward(object sender, Reward args) // Called when the user should be rewarded for interacting with the ad.
    {
        adListener.OnUserEarnedResurrectionReward();
        limitAdsPerDay.CountAdsShown();
    }
    #endregion
}
