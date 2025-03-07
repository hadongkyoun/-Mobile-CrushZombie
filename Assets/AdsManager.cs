using UnityEngine;

public class AdsManager : Singleton<AdsManager>
{
    public AdsInitializer adsInitalizer;
    public InterstitialAd interstitialAd;
    public RewardedAds rewardedAds;

    private void Start()
    {
        interstitialAd.LoadAd();
        rewardedAds.LoadAd();
    }
}
