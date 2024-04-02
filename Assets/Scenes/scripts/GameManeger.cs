using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using Unity.VisualScripting;

public class GameManeger : MonoBehaviour
{
    public static GameManeger instance { get; private set; }

    private int score = 0;
    public int highScore;
    public int highScore2 = 0;
    public Text scoreText;
    public Text binary;
    public Text highscoreText;
    public GameObject player;
    public GameObject piep;
    public GameObject Coin;
    public GameObject background;
    public GameObject background2;
    public GameObject playButton;
    public GameObject playButton2;
    public GameObject pauseButton;
    public GameObject restart;
    public GameObject resume;
    public GameObject mainmenue;
    public GameObject Gameover;
    public int countdowntime = 3;
    public Text countdown;
    Time time;
    TimeSpan playTime;
    int selectedCarcter;
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
        coin_ctr = 0;
        Gameover.SetActive(false);
        pauseButton.SetActive(false);
        resume.SetActive(false);
        restart.SetActive(false);
        mainmenue.SetActive(false);
        playButton2.SetActive(false);
        Application.targetFrameRate = 60;
        countdown.gameObject.SetActive(false);
        Pause();
    }
    
   public void Restart()
    {
        resume.SetActive(false);
        restart.SetActive(false);
        spawner.instance.clear();
        Play();
    }
    public void pausemenue()
    {
        resume.SetActive(true);
        restart.SetActive(true);
        mainmenue.SetActive(true);
        Time.timeScale = 0f;
        player.SetActive(false);
    }
    public void resumefun()
    {
        player.SetActive(true);
        Time.timeScale = 1f;
        resume.SetActive(false);
        restart.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        player.SetActive(false);
        
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
        spawner.instance.clear();
        piep.SetActive(false);
    }
    public void Play()
    {
        Time.timeScale = 0f;
        playTime = DateTime.Now.TimeOfDay;
        piep.SetActive(false);
        Coin.SetActive(false);
        player.SetActive(false);
        playButton.SetActive(false);
        countdown.gameObject.SetActive(true);

        coin_ctr = 0;
        pauseButton.SetActive(false);
        playButton2.SetActive(false);
        resume.SetActive(false);
        restart.SetActive(false);
        pauseButton.SetActive(true);
        score = 0;
        scoreText.color = new Color(255, 0, 0);
        scoreText.text = score.ToString();
        Gameover.SetActive(false);
        Coin.SetActive(true);
        piep.SetActive(true);
        background.SetActive(true);
        background2.SetActive(true);
        player.SetActive(true);
    }
    private void Update()
    {
        highscoreText.text = PlayerPrefs.GetInt("highscore").ToString();
        binary.text = dataManeger.instance.gameData.highScore.ToString();
        coin.text = coin_ctr.ToString();
        if(DateTime.Now.TimeOfDay.Minutes == playTime.Minutes)
        {
            countdown.text = (DateTime.Now.TimeOfDay.Seconds - playTime.Seconds).ToString();
        }
        else
        {
            countdown.text = (DateTime.Now.TimeOfDay.Seconds - playTime.Seconds ).ToString();
        }
        if (DateTime.Now.TimeOfDay.Seconds == playTime.Seconds + 4 )
        {
            Time.timeScale = 1f;
            countdown.gameObject.SetActive(false);
        }
    }
    public int coin_ctr;
    public Text coin;
    public void coinCounter()
    {
        coin_ctr++;
        dataManeger.instance.gameData.coin++;
    }
}
