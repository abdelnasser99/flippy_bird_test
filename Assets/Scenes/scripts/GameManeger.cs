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
        savetofile();
        Gameover.SetActive(false);
        pauseButton.SetActive(false);
        resume.SetActive(false);
        restart.SetActive(false);
        mainmenue.SetActive(false);
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
    [Serializable]
    public struct data
    {
        public string playername;
        public int highscore_birany;
    }
    public string directory = "Saves";
    data data2;
    public void savetofile()
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory + ".bin");
        }
        BinaryFormatter formater = new BinaryFormatter();
        FileStream savefile = File.Create("Saves.bin");
        formater.Serialize(savefile, data2);
        savefile.Close();
    }
        public void increaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        highScore = PlayerPrefs.GetInt("highscore");
        if (score > highScore)
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            scoreText.color = new Color(0, 0, 255);
        }
        else
        {
            scoreText.color = new Color(255, 0, 0);
        }
        if (data2.highscore_birany > highScore)
        {
            binary.color = new Color(0, 0, 255);
            data2.highscore_birany = highScore;
            savedfile.Close();
        }
    }
    public void GameOver()
    {
        Gameover.SetActive(true);
        playButton.SetActive(true);
        Pause();
    }
    private void Update()
    {
        highscoreText.text = PlayerPrefs.GetInt("highscore").ToString();
    }
 
    
}
