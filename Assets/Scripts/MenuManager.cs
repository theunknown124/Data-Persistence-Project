using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField playerNameInput; //Input field for player's name
    public TextMeshProUGUI bestScoreText; //Text for best score

    public string playerName;
    // Start is called before the first frame update
    void Start()
    {
        // Load the best score from the save file
        LoadBestScore();
    }

    public void ReadInputField(string playerNameInput)
    {
        playerName = playerNameInput;
        SaveDataManager.Instance.playerName = playerNameInput; // sets the player name from input   
    }

    public void StartGame()
    {
        // Save the player's name from the input field
        //string playerName = playerNameInput.text;

        //Save player's name from the input field
        //PlayerPrefs.SetString("CurrentPlayerName", playerNameInput.text);
        //SaveDataManager.Instance.playerName = playerName;
        //SaveDataManager.Instance.SavePlayerData();
        SaveDataManager.Instance.AddOrUpdateHighScore(playerName, 0);

       // Debug.Log("Best Score: " + SaveDataManager.Instance.bestScore);
        // Load the best score from the save file
        //LoadBestScore();

        // Load the game scene, index 1 is the main scene
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }
public void LoadBestScore()
{
    if (SaveDataManager.Instance == null)
    {
        Debug.LogError("SaveDataManager instance is null.");
        return;
    }

    // Load the saved data
    SaveDataManager.Instance.LoadPlayerData();

    // Get the player's name from the input field
    string currentPlayerName = playerNameInput.text;

    // Check if the player's name is in the high scores dictionary
    if (SaveDataManager.Instance.highScores.ContainsKey(currentPlayerName))
    {
        // Get the best score from the dictionary
        int playerScore = SaveDataManager.Instance.highScores[currentPlayerName];

        // Update the UI with the player's name and score
        bestScoreText.text = $"Best Score: {currentPlayerName} : {playerScore}";
    }
    else
    {
        // If the player doesn't exist, display a default message
        bestScoreText.text = $"No score found for {currentPlayerName}.";
    }
}

    public void ExitGame()
    {
        
        Application.Quit();
    
    }
}
