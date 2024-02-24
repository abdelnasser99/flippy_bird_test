using UnityEngine;
using UnityEngine.UI;
public class GameManeger : MonoBehaviour
{
    private int score;
    public Text scoreText;
    public player player;
    public GameObject playButton;
    public GameObject Gameover;
    public void Awake(){
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);
        Pause();
    }
    public void Play(){
        Time.timeScale = 1f;

        score =0 ;
        scoreText.text = score.ToString();

        Gameover.SetActive(false);
        playButton.SetActive(false);

        player.enabled = true;
        pieps[] PiepsList = FindObjectsOfType<pieps>(); 
        for(int i =0 ;i<PiepsList.Length;i++)
        {
            Destroy(PiepsList[i].gameObject);
        }
    }
    public void Pause(){
        Time.timeScale = 0f;
        player.enabled = false;  
    }
    public void increaseScore()
    {
        score++;
        scoreText.text=score.ToString();
    }
    public void GameOver()
    {
        Gameover.SetActive(true);
        playButton.SetActive(true);
        Pause();
    }
}
