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
    SaveData data;
    public void Start()
    {
        system = gameObject.AddComponent<binary_highscore>();
        data = system.Load();
        if (data.red == true)
        {
            red.gameObject.SetActive(false);
        }
        if (data.blue == true)
        {
            blue.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        //data = system.Load();
        coin.text = "Coins: " + data.coin.ToString();
        money = data.coin;
        if (data.selected_carcter == 1)
        {
            bird0.transform.localScale = new Vector3(0.7f, 0.7f, 1);

            bird1.transform.localScale = new Vector3(1, 1, 1);
            bird2.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(data.selected_carcter == 2)
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
        data = system.Load();
        if (data.blue==false && money>=50)
        {
            data.coin -= 50;
            data.blue = true;
            blue.gameObject.SetActive(false);
        }
        else
        {
            data.selected_carcter = 2;
        }
        system.Save(data);
    }
    public void gamemangment1()
    {
        data = system.Load();
        data.selected_carcter = 1;
        system.Save(data);
    }
    public void gamemangment3()
    {
        data = system.Load();
        if (data.red == false && money >= 100)
        {
            data.coin -= 100;
            data.red = true;
            red.gameObject.SetActive(false);
        }
        else
        {
            data.selected_carcter = 3;
        }
        system.Save(data);
    }
    public void main_menue()
    {
        data = system.Load();
        SceneManager.LoadScene("main menue");
        //data.coin=300;
        //system.Save(data);
    }
}
