using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Space.Abstract.Entity;

public class AdManager : SingletonMonoBehaviourObject<AdManager>
{
    private string bannerID = "ca-app-pub-6637454041741627/6934388448";
    private string intersititialID = "ca-app-pub-6637454041741627/2835361338";
    private string rewardedID = "ca-app-pub-6637454041741627/8270137955";
    private string rewardedInterstitialAd = "ca-app-pub-6637454041741627/8223419389";

    private BannerView _bannerAd;
    private InterstitialAd _interstitialAd;
    public bool _interstitialAdLoaded;
    private RewardedAd _rewardedAd;
    private RewardedInterstitialAd _rewardedInterstitialAd;

    public InterstitialAd InterstitialAd => _interstitialAd;
    public RewardedAd RewardedAd => _rewardedAd;
    public RewardedInterstitialAd RewardedInterstitialAd => _rewardedInterstitialAd;

    EventData _eventData;
    private void Awake()
    {
        SingletonThisObject(this,true,true);
        _eventData = Resources.Load("EventData") as EventData;
    }

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
        RequestInterstitial();
        RequestRewarded();
        RequestRewardedInterstitial();
    }

    #region BANNER
    private void RequestBanner()
    {
        // Create a 320x50 banner at the bottom  of the screen.
        _bannerAd = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        _bannerAd.LoadAd(request);
    }
    #endregion

    #region INTERSTITIAL

    private void RequestInterstitial()
    {
        _interstitialAdLoaded = false;
        // Initialize an InterstitialAd.
        _interstitialAd = new InterstitialAd(intersititialID);

        // Called when an ad request has successfully loaded.
        _interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        _interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        _interstitialAd.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        _interstitialAd.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        _interstitialAd.LoadAd(request);
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        _interstitialAdLoaded = true;
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitial();
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        _interstitialAdLoaded = false;

    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            RequestInterstitial();
        });
    }

    #endregion

    #region REWARDED
    private void RequestRewarded()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
        }
        // RewardedAd Catch.
        _rewardedAd = new RewardedAd(rewardedID);

        // Called when an ad request has successfully loaded.
        _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;

        // Called when an ad request failed to load.
        _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;

        // Called when an ad is shown.
        _rewardedAd.OnAdOpening += HandleRewardedAdOpening;

        // Called when an ad request failed to show.
        _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;

        // Called when the user should be rewarded for interacting with the ad.
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        // Called when the ad is closed.
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the rewarded ad with the request.
        _rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            RequestRewarded();
           
        });
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
       
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {

    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        //UpgradeCanvasController.Instance.GiveReward();
        //RequestRewarded();
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            RequestRewarded();
        });
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        //ÖDÜL VERME ALANI
        //On User Earnerd Reward 
        //GÝVE REWARD
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
         //   UIController.Instance.GiveReward();
        });
      
    }
    #endregion

    #region REWARDEDINTERSTITIAL
    private void RequestRewardedInterstitial()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        //Load the rewarded ad with the request.
        RewardedInterstitialAd.LoadAd(rewardedInterstitialAd, request, adLoadCallback);
    }

    private void adLoadCallback(RewardedInterstitialAd ad, AdFailedToLoadEventArgs error)
    {
        //throw new NotImplementedException();
        if (error == null)
        {
            _rewardedInterstitialAd = ad;

            _rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresent;
            _rewardedInterstitialAd.OnAdDidPresentFullScreenContent += HandleAdDidPresent;
            _rewardedInterstitialAd.OnAdDidDismissFullScreenContent += HandleAdDidDismiss;
            _rewardedInterstitialAd.OnPaidEvent += HandlePaidEvent;
        }
    }

    public void ShowRewardedInterstitialAd()
    {
        if (_rewardedInterstitialAd != null)
        {
            _rewardedInterstitialAd.Show(userEarnedRewardCallback);
        }
    }

    private void userEarnedRewardCallback(Reward reward)
    {
        ////TODO: Reward the user.
        ////Give Reward!  
        //UIController.Instance.WatchedAd();
        //_eventData.Player.PlayerWatchAd();
    }

    private void HandleAdFailedToPresent(object sender, AdErrorEventArgs args)
    {

    }

    private void HandleAdDidPresent(object sender, EventArgs args)
    {

    }

    private void HandleAdDidDismiss(object sender, EventArgs args)
    {
        RequestRewardedInterstitial();
        //TEKRAR REKLAM ÝSTEÐÝ ALANI
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args)
    {

    }

    #endregion

}
