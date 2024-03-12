using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class GameManeger : MonoBehaviour
{
    public static GameManeger instance { get; private set; }

    private int score = 0;
    public int highScore;
    public int highScore2 = 0;
    public Text scoreText;
    public Text binary;
    public Text highscoreText;
    public player player;
    public pieps piep;
    public coin_script Coin;
    public paralex background;
    public paralex background2;
    public GameObject playButton;
    public GameObject playButton2;
    public GameObject pauseButton;
    public GameObject restart;
    public GameObject resume;
    public GameObject mainmenue;
    public GameObject Gameover;
    public int countdowntime = 3;
    public Text countdown;
    //private SpriteRenderer spriteRenderer;
    int selectedCarcter;
    //public gameData gameData= new gameData();
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        dataManeger dataManeger = new dataManeger();
        coin_ctr = 0;
        Gameover.SetActive(false);
        pauseButton.SetActive(false);
        resume.SetActive(false);
        restart.SetActive(false);
        mainmenue.SetActive(false);
        playButton2.SetActive(false);
        Application.targetFrameRate = 60;
        Pause();
    }
    public void startcount()
    {
        piep.enabled = false;
        background.enabled = false;
        background2.enabled = false;
        Coin.enabled = false;
        StartCoroutine(countdowntostart());
    }
    IEnumerator countdowntostart()
    {
        playButton.SetActive(false);
        Time.timeScale = 1f;
        while (countdowntime > 0)
        {
            countdown.text = countdowntime.ToString();
            yield return new WaitForSeconds(1f);
            countdowntime--;
        }
        countdown.text = "GO!";
        yield return new WaitForSeconds(1f);
        Play();
        countdown.gameObject.SetActive(false);
    }
    public void Play()
    {

        coin_ctr = 0;
        //startcount();
        pauseButton.SetActive(false);
        playButton2.SetActive(false);
        resume.SetActive(false);
        restart.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        score = 0;
        scoreText.color = new Color(255, 0, 0);
        scoreText.text = score.ToString();
        Gameover.SetActive(false);
        Coin.enabled = true;
        piep.enabled = true;
        background.enabled = true;
        background2.enabled = true;
        //player.spriteRenderer= player.spriteRenderer.car
        player.enabled = true;

        //pieps[] PiepsList = FindObjectsOfType<pieps>(); //change here
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("obstecle_pipe");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i].gameObject);
        }
        GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("coin");
        for (int i = 0; i < gameObjects2.Length; i++)
        {
            Destroy(gameObjects2[i].gameObject);
        }
    }
    public void pausemenue()
    {
        resume.SetActive(true);
        restart.SetActive(true);
        mainmenue.SetActive(true);
        Time.timeScale = 0f;
        player.enabled = false;
    }
    public void resumefun()
    {
        player.enabled = true;
        Time.timeScale = 1f;
        resume.SetActive(false);
        restart.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }
    public void increaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        highScore = PlayerPrefs.GetInt("highscore");
        int binaryScore = dataManeger.instance.gameData.highScore;
        if (score > highScore)
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            scoreText.color = new Color(0, 0, 255);
            dataManeger.instance.gameData.highScore = score;
            //binary.text=score.ToString();

        }
        else
        {
            scoreText.color = new Color(255, 0, 0);
        }
    }
    public void GameOver()
    {
        Gameover.SetActive(true);
        playButton2.SetActive(true);
        dataManeger.instance.Save();
        Pause();
    }
    private void Update()
    {
        highscoreText.text = PlayerPrefs.GetInt("highscore").ToString();
        binary.text = dataManeger.instance.gameData.highScore.ToString();
        coin.text = coin_ctr.ToString();
    }
    public int coin_ctr;
    public Text coin;
    public void coinCounter()
    {
        coin_ctr++;
        dataManeger.instance.gameData.coin++;
    }

}
