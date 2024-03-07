using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
//using static binarycarcter;

public class binary_highscore : MonoBehaviour
{
    private const string saveFileName = "saveData20.dat";
    string savePath;
    //SaveData saveData = new SaveData();
    public SaveData Save(SaveData saveData)
    {
        
        BinaryFormatter formatter = new BinaryFormatter();
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);

        FileStream fileStream = null;

        try
        {
            fileStream = File.Create(savePath);
            formatter.Serialize(fileStream, saveData);
            Debug.Log("High score saved successfully!");
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

    public SaveData Load()
    {
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName);
        SaveData saveData = new SaveData();
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = null;

            try
            {
                fileStream = File.Open(savePath, FileMode.Open);
                saveData = (SaveData)formatter.Deserialize(fileStream);
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
