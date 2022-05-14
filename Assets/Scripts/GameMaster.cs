using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public ColorSchemes[] colorSchemes;
    public Sprite[] sprites;

    public PlayerController player;

    public SpriteRenderer playerSprite;

    public SpriteRenderer playerColor;

    public TextMeshProUGUI[] greenText;
    public TextMeshProUGUI[] redText;

    public Image[] greenImages;
    public Image[] redImages;

    private BannerView bannerView;

    void Start()
    {
        // TESTING
        //PlayerPrefs.SetFloat("HS",1000);
        //PlayerPrefs.SetInt("Coins", 1000);
        // END TESTING
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
        ChangeColorScheme(PlayerPrefs.GetInt("ColorScheme"));
        ChangeSprite(PlayerPrefs.GetInt("PlayerSprite"));
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4516766655285551/4843792361";
#else
        string adUnitId = "unexpected_platform";
#endif
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
        this.bannerView.Show();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }

        if (PlayerPrefs.GetInt("Sound") != 0)
        {
            GetComponent<AudioSource>().mute = true;
        }
        else
        {
            GetComponent<AudioSource>().mute = false;
        }
    }

    public void ChangeColorScheme(int cs)
    {
        PlayerPrefs.SetInt("ColorScheme", cs);
        Color32 ng = colorSchemes[cs].green;
        Color32 nr = colorSchemes[cs].red;
        playerColor.color = ng;
        Camera.main.backgroundColor = nr;
        player.greenColor = ng;
        player.redColor = nr;
        for (int i = 0; i < greenText.Length; i++)
        {
            greenText[i].color = ng;
        }
        for (int i = 0; i < redText.Length; i++)
        {
            redText[i].color = nr;
        }
        for (int i = 0; i < greenImages.Length; i++)
        {
            greenImages[i].color = ng;
        }
        for (int i = 0; i < redImages.Length; i++)
        {
            redImages[i].color = nr;
        }
    }

    public void ChangeSprite(int ps)
    {
        PlayerPrefs.SetInt("PlayerSprites", ps);
        playerSprite.sprite = sprites[ps];
    }

}

[System.Serializable]
public class ColorSchemes
{
    public Color32 green;
    public Color32 red;
}
