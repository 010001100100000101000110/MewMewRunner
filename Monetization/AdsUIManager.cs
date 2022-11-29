using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsUIManager : MonoBehaviour
{
    //t‰m‰ scripti vaihtelee nappien p‰‰ll‰ oloa riippuen siit‰, onko pelaaja katsonut p‰iv‰n reward mainoksen, onko h‰n jo katsonut resurrektio mainoksen (5) kertaa, onko h‰nell‰ tarpeeksi kr‰kkereit‰

    LimitAdsPerDay limitAdsPerDay;
    [SerializeField] Button dailyRewardButton;
    [SerializeField] Button resurrectionButton;
    [SerializeField] Button useCrackerButton;

    void Start()
    {
        limitAdsPerDay = FindObjectOfType<LimitAdsPerDay>();
    }

    void Update()
    {
        ShowCrackerAdButton();
        ShowResurrectionAdButton();
        ShowUseCrackerButton();
    }

    void ShowCrackerAdButton()
    {
        if (limitAdsPerDay.CanShowCrackerAd()) dailyRewardButton.interactable = true;
        if (!limitAdsPerDay.CanShowCrackerAd()) dailyRewardButton.interactable = false;
    }

    void ShowResurrectionAdButton()
    {
        if (limitAdsPerDay.CanShowResAd()) resurrectionButton.interactable = true;
        if (!limitAdsPerDay.CanShowResAd()) resurrectionButton.interactable = false;
    }

    void ShowUseCrackerButton()
    {
        if (PlayerPrefs.GetInt("CrackerCount") > 0) useCrackerButton.interactable = true;
        if (PlayerPrefs.GetInt("CrackerCount") <= 0) useCrackerButton.interactable = false;
    }
}
