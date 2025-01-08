using com.unity3d.mediation;
using UnityEngine;

public class IronSourceAds : MonoBehaviour
{
    [Header("Config General")]
    public int count_step_show_interstitial = 5;
    private int count_step=0;

    private LevelPlayBannerAd bannerAd;
    private LevelPlayInterstitialAd interstitialAd;

    public void On_Load()
    {
        string appKey = "20a7f7b6d";
        IronSource.Agent.init(appKey);
        IronSourceRewardedVideoEvents.onAdRewardedEvent += OnRewardedAdCompleted;
        IronSource.Agent.loadRewardedVideo();
        CreateBannerAd();
        CreateInterstitialAd();
        this.LoadInterstitialAd();
    }

    #region Banner Ads
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
        this.LoadBannerAd();
    }

    void LoadBannerAd() {
        bannerAd.LoadAd();
    }
    public void ShowBannerAd() {
        bannerAd.ShowAd();
    }
    public void HideBannerAd() {
        bannerAd.HideAd();
    }
    public void DestroyBannerAd() {
        bannerAd.DestroyAd();
    }

    void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo) {}
    void BannerOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) {}
    void BannerOnAdClickedEvent(LevelPlayAdInfo adInfo) {}
    void BannerOnAdDisplayedEvent(LevelPlayAdInfo adInfo) {}
    void BannerOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError adInfoError) {}
    void BannerOnAdCollapsedEvent(LevelPlayAdInfo adInfo) {}
    void BannerOnAdLeftApplicationEvent(LevelPlayAdInfo adInfo) {}
    void BannerOnAdExpandedEvent(LevelPlayAdInfo adInfo) {}
    #endregion

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

    #region InterstitialAd
    void CreateInterstitialAd() {
        interstitialAd= new LevelPlayInterstitialAd("interstitialAdUnitId");
        interstitialAd.OnAdLoaded += InterstitialOnAdLoadedEvent;
        interstitialAd.OnAdLoadFailed += InterstitialOnAdLoadFailedEvent;
        interstitialAd.OnAdDisplayed += InterstitialOnAdDisplayedEvent;
        interstitialAd.OnAdDisplayFailed += InterstitialOnAdDisplayFailedEvent;
        interstitialAd.OnAdClicked += InterstitialOnAdClickedEvent;
        interstitialAd.OnAdClosed += InterstitialOnAdClosedEvent;
        interstitialAd.OnAdInfoChanged += InterstitialOnAdInfoChangedEvent;
    }
    void LoadInterstitialAd() {
        interstitialAd.LoadAd();
    }

    public void ShowInterstitialAd() {
        if (interstitialAd.IsAdReady()) {
   		      interstitialAd.ShowAd();
        }
    }
    
    void DestroyInterstitialAd() {
        interstitialAd.DestroyAd();
    }
  
    void InterstitialOnAdLoadedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) { }
    void InterstitialOnAdClickedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError adInfoError) { }
    void InterstitialOnAdClosedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdInfoChangedEvent(LevelPlayAdInfo adInfo) { }
    #endregion
}
