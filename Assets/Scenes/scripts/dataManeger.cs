using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static binarycarcter;

[Serializable]
public class gameData
{
    public int highScore;
    public int coin;
    public bool blue;
    public bool red;
    public int selected_carcter;
}
public class dataManeger : MonoBehaviour
{
    public gameData gameData;
    public static dataManeger instance { get; private set; }
    private const string saveFileName = "Data.data";
    string savePath;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            SetDefault();
            instance = this;
            Load();
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void SetDefault()
    {
        gameData = new gameData();
        gameData.blue = false;
        gameData.red = false;
        gameData.coin = 0;
        gameData.highScore = 0;
        gameData.selected_carcter = 1;
        //Save();

    }
    public void Save()
    {

        BinaryFormatter formatter = new BinaryFormatter();
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);

        FileStream fileStream = null;

        try
        {
            fileStream = File.Create(savePath);
            formatter.Serialize(fileStream, gameData);
            Debug.Log("saved successfully!");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save high score: " + e.Message);
        }
        finally
        {
            if (fileStream != null)
                fileStream.Close();
        }

    }
    public void Load()
    {
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = null;

            try
            {
                fileStream = File.Open(savePath, FileMode.Open);
                gameData = (gameData)formatter.Deserialize(fileStream);
                Debug.Log("SaveData loaded successfully");
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load : " + e.Message);
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
        }
        else
        {
            //SetDefault();
            savePath = Path.Combine(Application.persistentDataPath, saveFileName);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Create(savePath);
            formatter.Serialize(fileStream, gameData);
            fileStream.Close();
            Debug.Log("No save file found. Initializing with default values.");
        }
    }
}
