using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class dataManeger : MonoBehaviour
{
    public static dataManeger instance { get; private set; }
    private void Awake()
    {
        if(instance == null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    [Serializable]
    public struct Data
    {
        public int highScore;
        public int coin;
        public bool blue;
        public bool red;
        public int selected_carcter;
    }

    private const string saveFileName = "save_Data.dat";
    string savePath;
    public Data Save(Data saveData)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);

        FileStream fileStream = null;

        try
        {
            fileStream = File.Create(savePath);
            formatter.Serialize(fileStream, saveData);
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
        return saveData;
    }
    public Data Load()
    {
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName);
        Data saveData = new Data();
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = null;

            try
            {
                fileStream = File.Open(savePath, FileMode.Open);
                saveData = (Data)formatter.Deserialize(fileStream);
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
            saveData.blue = false;
            saveData.red = false;
            saveData.coin = 0;
            saveData.highScore = 200;
            saveData.selected_carcter = 1;

            Debug.Log("No save file found. Initializing with default values.");
        }

        return saveData;
    }
}
