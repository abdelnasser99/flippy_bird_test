using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class binarycarcter : MonoBehaviour
{
    [Serializable]
    public struct SaveData
    {
        public int bluecarcters;
        public int redcarcters; 
        public int selected_carcter;
    }
    private const string saveFileName = "carcterData5.dat";
    public int LoadSelectedCarcter()
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
                if (String.IsNullOrEmpty(saveData.selected_carcter.ToString()))
                {
                    return 0;
                }
                else
                {
                    return saveData.selected_carcter;
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
    public void SaveHighScore(int highScore)
    {
        SaveData saveData = new SaveData();
        saveData.selected_carcter = highScore;

        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName);

        FileStream fileStream = null;

        try
        {
            fileStream = File.Create(savePath);
            formatter.Serialize(fileStream, saveData);
            Debug.Log("High score saved successfully!");
            fileStream.Close();
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
    public void bluecarcter()
    {
        SaveData saveData = new SaveData();
        saveData.bluecarcters = 1;

        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName);

        FileStream fileStream = null;

        try
        {
            fileStream = File.Create(savePath);
            formatter.Serialize(fileStream, saveData);
            Debug.Log("blue bird = "+ saveData.bluecarcters);
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
    public void redcarcter()
    {

        SaveData saveData = new SaveData();
        saveData.redcarcters = 1;

        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName);

        FileStream fileStream = null;

        try
        {
            fileStream = File.Create(savePath);
            formatter.Serialize(fileStream, saveData);
            Debug.Log("red bird = " + saveData.redcarcters);
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
    public int Loadboughtcarcter(string bird)
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
                if (bird == "blue" && saveData.bluecarcters==0)
                {
                    fileStream.Close();
                    bluecarcter();
                    return 0;
                }
                else if(bird =="red"&& saveData.redcarcters == 0)
                {
                    redcarcter();
                    fileStream.Close();
                    //saveData.redcarcters = 1;
                    //Debug.Log("red bird = " + saveData.redcarcters);
                    return 0;
                }
                else
                {
                    return 1;
                }
                /*switch (bird)
                {
                    case "blue":
                        if (saveData.bluecarcters == 0)
                        {
                            fileStream.Close();
                            bluecarcter();
                            //saveData.bluecarcters = 1;
                            //Debug.Log("blue bird = " + saveData.bluecarcters);
                            return 0;

                        }
                        break;
                    case "red":
                        if (saveData.redcarcters == 0)
                        {
                            redcarcter();
                            fileStream.Close();
                            //saveData.redcarcters = 1;
                            //Debug.Log("red bird = " + saveData.redcarcters);
                            return 0;
                           
                        }
                        break;
                }*/
                //Debug.Log("blue bird = " + saveData.bluecarcters);
                //Debug.Log("red bird = " + saveData.redcarcters);
                //Debug.Log( " not bought" );
                //return 1;
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
    public void test()
    {
        SaveData saveData = new SaveData();
        saveData.redcarcters = 0;
        saveData.bluecarcters = 0;

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
}
