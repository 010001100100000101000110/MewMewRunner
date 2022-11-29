using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialOnLaunch : MonoBehaviour
{
    //t‰m‰ scripti n‰ytt‰‰ interstitial mainoksen pelin avauksella kun pelaaja on k‰ynnist‰nyt sovelluksen viisi kertaa
    InterstitialAds interstitials;
    void Start()
    {
        interstitials = FindObjectOfType<InterstitialAds>();
    }  
    
    public void ShowInterstitialOnLaunch()
    {
        if (PlayerPrefs.GetInt("LaunchCount") >= 5)
        {
            interstitials.ShowInterstitial();
        }
    }
}
