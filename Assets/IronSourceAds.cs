using com.unity3d.mediation;
using UnityEngine;

public class IronSourceAds : MonoBehaviour
{
    private LevelPlayBannerAd bannerAd;
    void Start()
    {
        string appKey = "20a7f7b6d";
        IronSource.Agent.init(appKey);
        IronSourceRewardedVideoEvents.onAdRewardedEvent += OnRewardedAdCompleted;
        IronSource.Agent.loadRewardedVideo();
        CreateBannerAd();
    }

    void CreateBannerAd() {
        bannerAd = new LevelPlayBannerAd("3e0rwnhiuyvzf4ok", LevelPlayAdSize.BANNER,LevelPlayBannerPosition.TopCenter);
        bannerAd.OnAdLoaded += BannerOnAdLoadedEvent;
        bannerAd.OnAdLoadFailed += BannerOnAdLoadFailedEvent;
        bannerAd.OnAdDisplayed += BannerOnAdDisplayedEvent;
        bannerAd.OnAdDisplayFailed += BannerOnAdDisplayFailedEvent;
        bannerAd.OnAdClicked += BannerOnAdClickedEvent;
        bannerAd.OnAdCollapsed += BannerOnAdCollapsedEvent;
        bannerAd.OnAdLeftApplication += BannerOnAdLeftApplicationEvent;
        bannerAd.OnAdExpanded += BannerOnAdExpandedEvent;
    }

    void LoadBannerAd() {
        //Load the banner ad 
        bannerAd.LoadAd();
    }
    public void ShowBannerAd() {
        bannerAd.ShowAd();
    }
    void HideBannerAd() {
        bannerAd.HideAd();
    }
    void DestroyBannerAd() {
        //Destroy banner
        bannerAd.DestroyAd();
    }
    //Implement BannAd Events
    void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo) {}
    void BannerOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) {}
    void BannerOnAdClickedEvent(LevelPlayAdInfo adInfo) {}
    void BannerOnAdDisplayedEvent(LevelPlayAdInfo adInfo) {}
    void BannerOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError adInfoError) {}
    void BannerOnAdCollapsedEvent(LevelPlayAdInfo adInfo) {}
    void BannerOnAdLeftApplicationEvent(LevelPlayAdInfo adInfo) {}
    void BannerOnAdExpandedEvent(LevelPlayAdInfo adInfo) {}


    public void ShowRewardedVideo()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log("Quảng cáo chưa sẵn sàng.");
        }
    }

    private void OnRewardedAdCompleted(IronSourcePlacement placement,IronSourceAdInfo info)
    {
        Debug.Log($"Đã nhận thưởng: {placement.getRewardName()} {placement.getRewardAmount()}");
    }
}
