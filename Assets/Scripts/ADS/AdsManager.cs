using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdsManager : MonoBehaviour
{
    public InitializeAds initializeAds;
    public InterstitialAds interstitialAds;
    public RewardedAds rewardedAds;

    public static AdsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        rewardedAds.LoadRewardedAd();
        interstitialAds.LoadInterstitialAd();

    }

    public void ShowInterstitialAd()
    {
        interstitialAds.ShowInterstitialAd();
    }

    public void ShowRewardedAd()
    {
        rewardedAds.ShowRewardedAd();
    }

}

