using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class InitializeAdsLoadingScene : MonoBehaviour
{
    InterstitialAds interstitial;

    void Awake()
    {
        interstitial = FindObjectOfType<InterstitialAds>();

        #region
        //Call SetTagForChildDirectedTreatment on RequestConfiguration.Builder with the argument TagForChildDirectedTreatment.False
        //to indicate that you don't want your content treated as child-directed for the purposes of COPPA.

        //elikkäs näin vvvvv

        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.False).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        #endregion

        #region
        //Call SetTagForUnderAgeOfConsent on RequestConfiguration.Builder with the argument TagForUnderAgeOfConsent.True
        //to indicate that you want the request configuration to be handled in a manner suitable for users under the age of consent.

        //elikkäs näin vvvv

        RequestConfiguration requestConfiguration2 = new RequestConfiguration.Builder().SetTagForUnderAgeOfConsent(TagForUnderAgeOfConsent.True).build();
        MobileAds.SetRequestConfiguration(requestConfiguration2);
        #endregion

        #region
        //The following code configures a RequestConfiguration object to specify that ad content returned should correspond to a digital content label designation no higher than T
        RequestConfiguration requestConfiguration3 = new RequestConfiguration.Builder().SetMaxAdContentRating(MaxAdContentRating.T).build();
        MobileAds.SetRequestConfiguration(requestConfiguration3);

        #endregion

        //Initialize guuguu ads sdk
        MobileAds.Initialize(initStatus => { });

        interstitial.LoadInterstitialAd();
    }
}
