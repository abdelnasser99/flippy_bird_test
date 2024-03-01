using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class binary_highscore : MonoBehaviour
{
    [Serializable]
    public struct SaveData
    {
        public int highScore;
    }

    private const string saveFileName = "saveData.dat";

    public void SaveHighScore(int highScore)
    {
        SaveData saveData = new SaveData();
        saveData.highScore = highScore;

        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName);

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
    }

    public int LoadHighScore()
    {
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = null;

            try
            {
                fileStream = File.Open(savePath, FileMode.Open);
                SaveData saveData = (SaveData)formatter.Deserialize(fileStream);
                if (saveData.highScore == null)
                {
                    return 0;
                }
                else
                {
                    return saveData.highScore;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load high score: " + e.Message);
                return 0; 
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
        }
        else
        {
            Debug.LogWarning("Save file does not exist. Returning default high score.");
            return 0;
        }
    }
}
