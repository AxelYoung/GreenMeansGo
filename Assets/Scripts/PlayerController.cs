using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed;
    private float rotSpeed;

    private Rigidbody2D rb;

    public bool green = true;

    public Color32 greenColor;
    public Color32 redColor;

    private SpriteRenderer sr;

    public Transform scoreText;

    private float score;

    public bool invert;

    private float startSpeed;

    public bool canMove = false;

    public GameObject gameOverScreen;

    public AudioSource au;

    public AudioClip colorSwap;
    public AudioClip coin;
    public AudioClip death;

    public int dupCoins;

    // Start is called before the first frame update
    void Start()
    {
        startSpeed = playerSpeed;
        rb = GetComponent<Rigidbody2D>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed = startSpeed + (transform.position.y / 800);
        rotSpeed = playerSpeed * 10;
        // Movement
        if (canMove)
        {
            if (green)
            {
                rb.velocity = Vector2.up * playerSpeed;
                transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }

        if (canMove)
        {
            // Phone Controls
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    if (!invert)
                    {
                        Red();
                    }
                    else
                    {
                        Green();
                    }

                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    if (!invert)
                    {
                        Green();
                    }
                    else
                    {
                        Red();
                    }
                }
            }

            // PC controls
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!invert)
                {
                    Red();
                }
                else
                {
                    Green();
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (!invert)
                {
                    Green();
                }
                else
                {
                    Red();
                }
            }
        }

        score = Mathf.RoundToInt(transform.position.y * 5);
        if (score / 10 != Mathf.RoundToInt(score / 10))
        {
            scoreText.transform.GetComponent<TextMeshProUGUI>().text = (score / 10).ToString();
        }
        else
        {
            scoreText.transform.GetComponent<TextMeshProUGUI>().text = (score / 10).ToString() + ".0";
        }
    }

    void LateUpdate()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        screenPos.y += 150;
        scoreText.position = screenPos;
    }

    public void Green()
    {
        green = true;
        ColorSwap(redColor, greenColor);
    }

    public void Red()
    {
        green = false;
        ColorSwap(greenColor, redColor);
    }

    public void ColorSwap(Color32 pColor, Color32 bColor)
    {
        if(Camera.main.backgroundColor != bColor)
        {
            if(PlayerPrefs.GetInt("Sound") != 2)
            {
                Vibration.Vibrate(20);
            }
            au.PlayOneShot(colorSwap);
            Camera.main.backgroundColor = bColor;
            scoreText.GetComponent<TextMeshProUGUI>().color = pColor;
            sr.color = pColor;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            Die();
        }
        if(collision.tag == "Switch")
        {
            invert = !invert;
            if (invert)
            {
                green = false;
                ColorSwap(greenColor, redColor);
            }
            else
            {
                green = true;
                ColorSwap(redColor, greenColor);
            }
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Coin")
        {
            if (PlayerPrefs.GetInt("Sound") != 2)
            {
                Vibration.Vibrate(10);
            }
            au.PlayOneShot(coin);
            Destroy(collision.gameObject);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
            dupCoins += 1;
        }
    }

    public void Die()
    {
        if (PlayerPrefs.GetInt("Sound") != 2)
        {
            Vibration.Vibrate(100);
        }
        au.PlayOneShot(death);
        ColorSwap(greenColor,redColor);
        if(score / 10 == Mathf.RoundToInt(score / 10))
        {
            gameOverScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "YOUR SCORE:\n" + (score / 10).ToString() + ".0";
        }
        else
        {
            gameOverScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "YOUR SCORE:\n" + (score / 10).ToString();
        }

        if(score / 10 > PlayerPrefs.GetFloat("HS"))
        {
            PlayerPrefs.SetFloat("HS", score / 10);
        }
        if (PlayerPrefs.GetFloat("HS") == Mathf.RoundToInt(PlayerPrefs.GetFloat("HS")))
        {
            gameOverScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "HIGHSCORE:\n" + PlayerPrefs.GetFloat("HS").ToString() + ".0";
        }
        else
        {
            gameOverScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "HIGHSCORE:\n" + PlayerPrefs.GetFloat("HS").ToString();
        }
        gameOverScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "COINS:" + PlayerPrefs.GetInt("Coins");
        gameOverScreen.SetActive(true);
        canMove = false;
        gameObject.SetActive(false);
    }
}
