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
    private static GameManeger _instance;
    public static GameManeger Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManeger>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(GameManeger).Name);
                    _instance = singletonObject.AddComponent<GameManeger>();
                }
            }
            return _instance;
        }
    }
    private int score = 0;
    public int highScore;
    public Text scoreText;
    public Text binary;
    public Text highscoreText;
    public player player;
    public pieps piep;
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
    public BinaryFormatter formater;
    public FileStream savedfile;
    public void Awake()
    {
        Gameover.SetActive(false);
        pauseButton.SetActive(false);
        resume.SetActive(false);
        restart.SetActive(false);
        mainmenue.SetActive(false);
        playButton2.SetActive(false);
        //binary_highscore tempscore = new binary_highscore();    
        //tempscore.SaveHighScore(0);
        Application.targetFrameRate = 60;
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);//change here
        Pause();
    }
    public void startcount()
    {
        piep.enabled = false;
        background.enabled = false;
        background2.enabled = false;
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

        piep.enabled = true;
        background.enabled = true;
        background2.enabled = true;
        player.enabled = true;
     
       //pieps[] PiepsList = FindObjectsOfType<pieps>(); //change here
       GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("obstecle_pipe");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i].gameObject);
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
    binary_highscore tempscore = new binary_highscore();
    public void increaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        highScore = PlayerPrefs.GetInt("highscore");
        int binaryScore = tempscore.LoadHighScore();

        if (score > highScore)
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            scoreText.color = new Color(0, 0, 255);
            tempscore.SaveHighScore(score);
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
        Pause();
    }
    private void Update()
    {
        highscoreText.text = PlayerPrefs.GetInt("highscore").ToString();
        binary.text= tempscore.LoadHighScore().ToString();
    }
 
    
}
