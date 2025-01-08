using com.unity3d.mediation;
using UnityEngine;
using UnityEngine.Events;

public class IronSourceAds : MonoBehaviour
{
    [Header("Config General")]
    public int count_step_show_interstitial = 5;
    private int count_step = 0;

    [Header("Emplement Ui Ads")]
    public GameObject[] emplement_Ads;

    [Header("Config IronSource")]
    public string app_key;
    public string id_banner;
    public string id_video;
    public string id_rewarded;
    public UnityAction onRewardedSuccess;
    private LevelPlayBannerAd bannerAd;
    private LevelPlayInterstitialAd interstitialAd;
    private bool is_ads = false;

    public void On_Load()
    {
        if (PlayerPrefs.GetInt("is_ads", 0) == 0)
            this.is_ads = true;
        else
            this.is_ads = false;

        if (this.is_ads)
        {
            IronSource.Agent.init(this.app_key);
            IronSourceRewardedVideoEvents.onAdRewardedEvent += OnRewardedAdCompleted;
            IronSource.Agent.loadRewardedVideo();
            CreateBannerAd();
            CreateInterstitialAd();
            this.LoadInterstitialAd();
        }
        this.Check_Emplement_Ads();


    }

    private void Check_Emplement_Ads()
    {
        if (this.emplement_Ads.Length != 0)
        {
            for (int i = 0; i < this.emplement_Ads.Length; i++)
            {
                if (this.is_ads)
                    this.emplement_Ads[i].SetActive(true);
                else
                    this.emplement_Ads[i].SetActive(false);
            }
        }
    }

    #region Banner Ads
    void CreateBannerAd()
    {
        bannerAd = new LevelPlayBannerAd(this.id_banner, LevelPlayAdSize.BANNER, LevelPlayBannerPosition.TopCenter);
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

    void LoadBannerAd()
    {
        bannerAd.LoadAd();
    }
    public void ShowBannerAd()
    {
        bannerAd.ShowAd();
    }
    public void HideBannerAd()
    {
        bannerAd.HideAd();
    }
    public void DestroyBannerAd()
    {
        bannerAd.DestroyAd();
    }

    void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) { }
    void BannerOnAdClickedEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError adInfoError) { }
    void BannerOnAdCollapsedEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdLeftApplicationEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdExpandedEvent(LevelPlayAdInfo adInfo) { }
    #endregion

    public void ShowRewardedVideo()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo(this.id_rewarded);
        }
        else
        {
            Debug.Log("Quảng cáo chưa sẵn sàng.");
        }
    }

    private void OnRewardedAdCompleted(IronSourcePlacement placement, IronSourceAdInfo info)
    {
        this.onRewardedSuccess?.Invoke();
        Debug.Log($"Đã nhận thưởng: {placement.getRewardName()} {placement.getRewardAmount()}");
    }

    #region InterstitialAd
    void CreateInterstitialAd()
    {
        interstitialAd = new LevelPlayInterstitialAd(this.id_video);
        interstitialAd.OnAdLoaded += InterstitialOnAdLoadedEvent;
        interstitialAd.OnAdLoadFailed += InterstitialOnAdLoadFailedEvent;
        interstitialAd.OnAdDisplayed += InterstitialOnAdDisplayedEvent;
        interstitialAd.OnAdDisplayFailed += InterstitialOnAdDisplayFailedEvent;
        interstitialAd.OnAdClicked += InterstitialOnAdClickedEvent;
        interstitialAd.OnAdClosed += InterstitialOnAdClosedEvent;
        interstitialAd.OnAdInfoChanged += InterstitialOnAdInfoChangedEvent;
    }

    void LoadInterstitialAd()
    {
        interstitialAd.LoadAd();
    }

    public void Show_Video_Ads()
    {
        this.count_step++;
        if (this.count_step > this.count_step_show_interstitial)
        {
            this.ShowInterstitialAd();
        }
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd.IsAdReady())
        {
            interstitialAd.ShowAd();
        }
    }

    void DestroyInterstitialAd()
    {
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

    public void RemoveAds()
    {
        this.HideBannerAd();
        PlayerPrefs.SetInt("is_ads", 1);
        this.is_ads = false;
        this.Check_Emplement_Ads();
    }

    public bool get_status_ads()
    {
        return this.is_ads;
    }
}
