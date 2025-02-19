using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance;
    public string currentPlayerName;
    public int currentPlayerScore;

    private Dictionary<string, int> playerScores = new Dictionary<string, int>();
    
    private void Awake() 
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayerData();
            
    }

    class SaveData
    {
        public Dictionary<string, int> playerScores;
    }

    public void SavePlayerData()
    {
        //This method will save the player's name and best score
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.bestScore = bestScore;
        
        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerData()
    {
        //This method will load the player's name and best score
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            
            playerName = data.playerName;
            bestScore = data.bestScore;
        }
        else
        {
            playerName = "Player"; //Default player name if no save data exists
            bestScore = 0; // Default best score if no save data exists
        }
    }

    public void DeleteSavedData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path)) // Check if the file exists
        {
            File.Delete(path); // Delete the file
            Debug.Log("Saved data deleted."); // Log message to console
        }
        else
        {
            Debug.Log("No saved data to delete."); // Log message if no file exists
        }
    }
    
}
