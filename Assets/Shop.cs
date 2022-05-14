using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI[] skinsText;
    public TextMeshProUGUI[] colorsText;

    public GameMaster gm;

    void Start()
    {
        PlayerPrefs.SetInt("Skin0", 1);
        PlayerPrefs.SetInt("ColorScheme0", 1);
    }
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < skinsText.Length; i++)
        {
            if(PlayerPrefs.GetInt("Skin" + i) == 1)
            {
                skinsText[i].text = "";
            }
            if (PlayerPrefs.GetInt("PlayerSprite") == i)
            {
                skinsText[i].text = "ACTIVE";
            }
        }
        for (int i = 0; i < colorsText.Length; i++)
        {
            if (PlayerPrefs.GetInt("ColorScheme" + i) == 1)
            {
                colorsText[i].text = "";
            }
            if (PlayerPrefs.GetInt("ColorScheme") == i)
            {
                colorsText[i].text = "ACTIVE";
            }
        }
        if (PlayerPrefs.GetFloat("HS") >= 50)
        {
            PlayerPrefs.SetInt("ColorScheme1", 1);
        }
        if (PlayerPrefs.GetFloat("HS") >= 100)
        {
            PlayerPrefs.SetInt("ColorScheme2", 1);
        }
        if (PlayerPrefs.GetFloat("HS") >= 150)
        {
            PlayerPrefs.SetInt("ColorScheme3", 1);
        }
        if (PlayerPrefs.GetFloat("HS") >= 250)
        {
            PlayerPrefs.SetInt("ColorScheme4", 1);
        }
        gm.ChangeColorScheme(PlayerPrefs.GetInt("ColorScheme"));
        gm.ChangeSprite(PlayerPrefs.GetInt("PlayerSprite"));
    }

    public void SetDefaultCS()
    {
        PlayerPrefs.SetInt("ColorScheme", 0);
    }

    public void Set50HS()
    {
        if(PlayerPrefs.GetInt("ColorScheme1") == 1)
        {
            PlayerPrefs.SetInt("ColorScheme", 1);
        }
    }

    public void Set100HS()
    {
        if (PlayerPrefs.GetInt("ColorScheme2") == 1)
        {
            PlayerPrefs.SetInt("ColorScheme", 2);
        }
    }

    public void Set150HS()
    {
        if (PlayerPrefs.GetInt("ColorScheme3") == 1)
        {
            PlayerPrefs.SetInt("ColorScheme", 3);
        }
    }

    public void Set250HS()
    {
        if (PlayerPrefs.GetInt("ColorScheme4") == 1)
        {
            PlayerPrefs.SetInt("ColorScheme", 4);
        }
    }

    public void SetDefaultSkin()
    {
        PlayerPrefs.SetInt("PlayerSprite", 0);
    }

    public void Buy25()
    {
        if (PlayerPrefs.GetInt("Skin1") == 0)
        {
            if (PlayerPrefs.GetInt("Coins") >= 25)
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 25);
                PlayerPrefs.SetInt("Skin1", 1);
                PlayerPrefs.SetInt("PlayerSprite", 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("PlayerSprite", 1);
        }

    }

    public void Buy50()
    {
        if (PlayerPrefs.GetInt("Skin2") == 0)
        {
            if (PlayerPrefs.GetInt("Coins") >= 50)
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 50);
                PlayerPrefs.SetInt("Skin2", 1);
                PlayerPrefs.SetInt("PlayerSprite", 2);
            }
        }
        else
        {
            PlayerPrefs.SetInt("PlayerSprite", 2);
        }
    }

    public void Buy100()
    {
        if (PlayerPrefs.GetInt("Skin3") == 0)
        {
            if (PlayerPrefs.GetInt("Coins") >= 100)
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 100);
                PlayerPrefs.SetInt("Skin3", 1);
                PlayerPrefs.SetInt("PlayerSprite", 3);
            }
        }
        else
        {
            PlayerPrefs.SetInt("PlayerSprite", 3);
        }
    }

    public void Buy150()
    {
        if (PlayerPrefs.GetInt("Skin4") == 0)
        {
            if (PlayerPrefs.GetInt("Coins") >= 150)
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 150);
                PlayerPrefs.SetInt("Skin4", 1);
                PlayerPrefs.SetInt("PlayerSprite", 4);
            }
        }
        else
        {
            PlayerPrefs.SetInt("PlayerSprite", 4);
        }
    }
}
