using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;
using System.Linq;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance;
    public string playerName;
    public int bestScore;

    public Dictionary<string, int> highScores = new Dictionary<string, int>();
    
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

        Debug.Log("Persistent data path: " + Application.persistentDataPath); 
            
    }

    class SaveData
    {
        //public string playerName;
        //public int bestScore;
        public List<KeyValuePair<string, int>> highScoresList = new List<KeyValuePair<string, int>>();
    }


    public void AddOrUpdateHighScore(string playerName, int score)
{
    if (highScores.ContainsKey(playerName))
    {
        // Update the high score if the current score is higher
        if (score > highScores[playerName])
        {
            highScores[playerName] = score;
            Debug.Log($"Updated score for {playerName}: {score}");
        }
    }
    else
    {
        // Add a new entry to the dictionary
        highScores.Add(playerName, score);
        Debug.Log($"Added new player {playerName} with score: {score}");
    }

    SavePlayerData(); // Save the updated dictionary
}

    public Dictionary<string, int> GetHighScores()
    {
        return highScores;
    }

    public void SavePlayerData()
{
    // Debug: Check if highScores is populated
    if (highScores.Count == 0)
    {
        Debug.LogWarning("highScores dictionary is empty. Nothing to save.");
        return;
    }

    // Create a SaveData object
    SaveData data = new SaveData();
    data.highScoresList = highScores.ToList();

    // Debug: Check if highScoresList is populated
    if (data.highScoresList.Count == 0)
    {
        Debug.LogWarning("highScoresList is empty. Check the dictionary.");
        return;
    }

    // Serialize the SaveData object to JSON
    string json = JsonUtility.ToJson(data);

    // Debug: Check the JSON output
    Debug.Log("Saving JSON: " + json);

    // Save the JSON to a file
    string path = Application.persistentDataPath + "/savefile.json";
    File.WriteAllText(path, json);

    // Debug: Confirm the file was saved
    Debug.Log("Save file written to: " + path);
}

public void LoadPlayerData()
{
    string path = Application.persistentDataPath + "/savefile.json";
    if (File.Exists(path))
    {
        try
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Check if highScoresList is null
            if (data.highScoresList == null)
            {
                data.highScoresList = new List<KeyValuePair<string, int>>();
            }

            // Convert the list to a dictionary
            highScores = data.highScoresList.ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load save data: " + e.Message);
            highScores = new Dictionary<string, int>(); // Initialize an empty dictionary on error
        }
    }
    else
    {
        highScores = new Dictionary<string, int>(); // Initialize an empty dictionary if no save file exists
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
