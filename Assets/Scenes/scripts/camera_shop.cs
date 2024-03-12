using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class camera_shop : MonoBehaviour
{
    public Text coin;
    binary_highscore system;
    public Button bird0;
    public Button bird1;
    public Button bird2;
    public Text blue;
    public Text red;    
    int money;
    //SaveData data;
    public void Start()
    {
        if (dataManeger.instance.gameData.red == true)
        {
            red.gameObject.SetActive(false);
        }
        if (dataManeger.instance.gameData.blue == true)
        {
            blue.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        //data = system.Load();
        coin.text = "Coins: " + dataManeger.instance.gameData.coin.ToString();
        money = dataManeger.instance.gameData.coin;
        if (dataManeger.instance.gameData.selected_carcter == 1)
        {
            bird0.transform.localScale = new Vector3(0.7f, 0.7f, 1);

            bird1.transform.localScale = new Vector3(1, 1, 1);
            bird2.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(dataManeger.instance.gameData.selected_carcter == 2)
        {
            bird1.transform.localScale = new Vector3(0.7f, 0.7f, 1);

            bird0.transform.localScale = new Vector3(1, 1, 1);
            bird2.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            bird2.transform.localScale = new Vector3(0.7f, 0.7f, 1);

            bird1.transform.localScale = new Vector3(1, 1, 1);
            bird0.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void gamemangment2()
    {
        
        if (dataManeger.instance.gameData.blue==false && money>=50)
        {
            dataManeger.instance.gameData.coin -= 50;
            dataManeger.instance.gameData.blue = true;
            blue.gameObject.SetActive(false);
        }
        else
        {
            dataManeger.instance.gameData.selected_carcter = 2;
        }
        dataManeger.instance.Save();
    }
    public void gamemangment1()
    {
        dataManeger.instance.gameData.selected_carcter = 1;
        dataManeger.instance.Save();
    }
    public void gamemangment3()
    {
        if (dataManeger.instance.gameData.red == false && money >= 100)
        {
            dataManeger.instance.gameData.coin -= 100;
            dataManeger.instance.gameData.red = true;
            red.gameObject.SetActive(false);
        }
        else
        {
            dataManeger.instance.gameData.selected_carcter = 3;
        }
        dataManeger.instance.Save();
    }
    public void main_menue()
    {
        SceneManager.LoadScene("main menue");
        //data.coin=300;
        //system.Save(data);
    }
}
