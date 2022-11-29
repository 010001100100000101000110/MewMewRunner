using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdsHelper : MonoBehaviour
{        
    //tämä scripti sisältää aputekijöitä banner interstitial ja rewarded mainos scripteille
    
    [HideInInspector] public AdListener adListener;
    SetRetryAmount setRetryAmount;
    

    void Awake()
    {
        adListener = FindObjectOfType<AdListener>();
        setRetryAmount = FindObjectOfType<SetRetryAmount>();
    }
    public enum AdType
    {
        Banner,
        Interstitial,
        CrackerReward,
        ResurrectionReward
    }

    public int RetryAmount()
    {
        return setRetryAmount.AmountOfRetriesToLoadOrShowAds;
    }

    public int GetNPAValue()
    {
        return PlayerPrefs.GetInt("npa");
    }

    #region GetAdUnitId
    public string GetAdUnitId(AdType type)
    {
        string adUnitId;

        //Banner Ad ID
        string bannerAdUnitId;
#if UNITY_ANDROID
        bannerAdUnitId = "ca-app-pub-9886246352735904/2650192068";
#elif UNITY_IPHONE
        bannerAdUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        bannerAdUnitId = "unexpected_platform";
#endif
        //Interstitial Ad ID
        string interstitialAdUnitId;
#if UNITY_ANDROID
        interstitialAdUnitId = "ca-app-pub-9886246352735904/5482091731";
#elif UNITY_IPHONE
        interstitialAdUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        interstitialAdUnitId = "unexpected_platform";
#endif
        //Cracker Rewarded Ad ID
        string crackerAdUnitId;
#if UNITY_ANDROID
        crackerAdUnitId = "ca-app-pub-9886246352735904/3977438377";
#elif UNITY_IPHONE
        crackerAdUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        crackerAdUnitId = "unexpected_platform";
#endif
        //Resurrection Rewarded Ad ID
        string resAdUnitId;
#if UNITY_ANDROID
        resAdUnitId = "ca-app-pub-9886246352735904/9351331789";
#elif UNITY_IPHONE
        resAdUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        resAdUnitId = "unexpected_platform";
#endif

        switch(type)
        {
            case AdType.Banner:
                adUnitId = bannerAdUnitId;
                break;
            case AdType.Interstitial:
                adUnitId = interstitialAdUnitId;
                break;
            case AdType.CrackerReward:
                adUnitId = crackerAdUnitId;
                break;
            case AdType.ResurrectionReward:
                adUnitId = resAdUnitId;
                break;
            default:
                adUnitId = null;
                break;
        }
        return adUnitId;
    }
    #endregion
}
