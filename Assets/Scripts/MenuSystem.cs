//using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour {

    public GameObject menuItems;
    public GameObject playerText;

    private bool menuOn;

    private PlayerController player;

    public TextMeshProUGUI hs;

    public TextMeshProUGUI cns;

    public Image soundIcon;

    public Sprite[] soundIcons;

    public GameObject shop;

    //private InterstitialAd interstitial;

    //private RewardedAd rewardedAd;

    public GameObject twoTimes;
    /*

    private void RequestInterstitial()
    {
        string radUnitId;
#if UNITY_ANDROID
        radUnitId = "ca-app-pub-4516766655285551/5965302341";
#else
        radUnitId = "unexpected_platform";
#endif
        this.rewardedAd = new RewardedAd(radUnitId);
        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest rrequest = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(rrequest);
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4516766655285551/8591465680";
#else
        string adUnitId = "unexpected_platform";
#endif
        this.interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
    */

    // Start is called before the first frame update
    void Start() {
        //RequestInterstitial();
        menuOn = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (PlayerPrefs.GetFloat("HS") == Mathf.RoundToInt(PlayerPrefs.GetFloat("HS"))) {
            hs.text = "HIGHSCORE:\n" + PlayerPrefs.GetFloat("HS").ToString() + ".0";
        } else {
            hs.text = "HIGHSCORE:\n" + PlayerPrefs.GetFloat("HS").ToString();
        }
        cns.text = "COINS:" + PlayerPrefs.GetInt("Coins");
    }

    // Update is called once per frame
    void Update() {
        if (menuOn) {
            if (Input.GetKeyUp(KeyCode.Space)) {
                StartGame();
            }
            soundIcon.sprite = soundIcons[PlayerPrefs.GetInt("Sound")];
        }
    }

    public void StartGame() {
        menuItems.SetActive(false);
        playerText.SetActive(true);
        player.Green();
        player.canMove = true;
        menuOn = false;
    }

    public void RestartScene() {
        if (PlayerPrefs.GetInt("Sound") != 2) {
            Vibration.Vibrate(10);
        }
        //if (this.interstitial.IsLoaded())
        //{
        //    this.interstitial.Show();
        //}
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeSound() {
        if (PlayerPrefs.GetInt("Sound") < 2) {
            PlayerPrefs.SetInt("Sound", PlayerPrefs.GetInt("Sound") + 1);
        } else {
            PlayerPrefs.SetInt("Sound", 0);
        }
        print("Something");
    }

    public void OpenShop() {
        shop.SetActive(true);
    }

    public void CloseShop() {
        shop.SetActive(false);
    }

    public void LoadRewardedAd() {
        //if (this.rewardedAd.IsLoaded())
        //{
        //    this.rewardedAd.Show();
        //}
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args) {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    //public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    //{
    //    MonoBehaviour.print(
    //        "HandleRewardedAdFailedToLoad event received with message: "
    //                         + args.Message);
    //}

    public void HandleRewardedAdOpening(object sender, EventArgs args) {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    //public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    //{
    //    MonoBehaviour.print(
    //        "HandleRewardedAdFailedToShow event received with message: "
    //                        + args.Message);
    //}

    public void HandleRewardedAdClosed(object sender, EventArgs args) {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }


    /*
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + player.dupCoins);
        cns.text = "COINS:" + PlayerPrefs.GetInt("Coins");
        twoTimes.SetActive(false);
    }
    */
}
