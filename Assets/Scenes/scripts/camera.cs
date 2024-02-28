using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class camera : MonoBehaviour
{
   public void gamemangment()
    {
        SceneManager.LoadScene("flappy bird");
    }
    public void gamemangment2()
    {
        SceneManager.LoadScene("main menue");
    }
    public void quit()
    {
        Application.Quit();
     }
} 
