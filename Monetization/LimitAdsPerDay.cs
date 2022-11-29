using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LimitAdsPerDay : MonoBehaviour
{
    //t‰ss‰ scriptiss‰ rajoitetaan p‰iv‰ss‰ katseltavien mainosten m‰‰r‰‰
    RewardedAds rewarded;
    [SerializeField] int resurrectionAdLimit;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("LastDateCrackerAdWatched")) PlayerPrefs.SetString("LastDateCrackerAdWatched", "2000-01-01");
        PlayerPrefs.SetInt("AdsShownPerRun", 0);
    }

    void Start()
    {
        rewarded = FindObjectOfType<RewardedAds>();       
    }
    void ResetValuesForTesting()
    {
        PlayerPrefs.SetInt("AdsShownPerRun", 0);
        PlayerPrefs.SetString("LastDateCrackerAdWatched", "2000-01-01");
        PlayerPrefs.SetInt("CrackerCount", 0);
    }

    public bool CanShowCrackerAd()
    {
        if (DateTime.Now > DateTime.ParseExact(PlayerPrefs.GetString("LastDateCrackerAdWatched", "0000-00-00"), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).AddDays(1)) return true;
        else return false;
    }

    public void OnUserWatchedDailyRewardAd()
    {
        PlayerPrefs.SetString("LastDateCrackerAdWatched", DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0'));
    }

    public void ShowDailyRewardAd() //nappiin
    {
        if (CanShowCrackerAd()) rewarded.ShowRewardedAd(RewardedAds.RewardAdType.Cracker);
    }

    //daily resurrection ad limitation

    public bool CanShowResAd()
    {
        if (PlayerPrefs.GetInt("AdsShownPerRun") < resurrectionAdLimit) return true;
        else return false;
    }

    public void CountAdsShown()
    {
        if (PlayerPrefs.HasKey("AdsShownPerRun"))
        {
            int adCount = PlayerPrefs.GetInt("AdsShownPerRun");
            adCount += 1;
            PlayerPrefs.SetInt("AdsShownPerRun", adCount);
        }
        else PlayerPrefs.SetInt("AdsShownPerRun", 1);
    }

    public void ResetAdsShownPerRun()
    {
        PlayerPrefs.SetInt("AdsShownPerRun", 0);
    }
    
    public void ShowResurrectionAd() //nappiin
    {
        if (CanShowResAd()) rewarded.ShowRewardedAd(RewardedAds.RewardAdType.Resurrection);
    }
}
