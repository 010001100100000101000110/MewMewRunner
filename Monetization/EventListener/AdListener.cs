using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdListener : MonoBehaviour
{
    [SerializeField] GameEvent OnAdOpeningEvent;
    [SerializeField] GameEvent OnAdClosedEvent;

    [SerializeField] GameEvent RewardedOnUserEarnedResurrectionRewardEvent;

    public void OnAdOpening()
    {
        OnAdOpeningEvent?.Raise();
    }

    public void OnAdClosed()
    {
        OnAdClosedEvent?.Raise();
    }
    public void OnUserEarnedResurrectionReward()
    {
        RewardedOnUserEarnedResurrectionRewardEvent?.Raise();
    }
}
