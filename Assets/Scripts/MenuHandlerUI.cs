using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandlerUI : MonoBehaviour
{
    public TMP_InputField playerNameInput; // Input field for entering the player's name
    public TextMeshProUGUI bestScoreText; // Text field to display the highest score

    void Start()
    {
        LoadBestScore(); // Load the highest score when the game starts
    }

    public void StartGame()
    {
        // Save the player's name to PlayerPrefs for use in the game scene
        PlayerPrefs.SetString("CurrentPlayerName", playerNameInput.text);

        // Load the game scene (index 1 in the build settings)
        SceneManager.LoadScene(1);
    }

    public void LoadBestScore()
    {
        // Display the highest score and corresponding player name from MainGameManager
        bestScoreText.text = "Best Score: " + SaveDataManager.Instance.bestScore + " Name: " + SaveDataManager.Instance.playerName;
    }

    public void Quit()
    {
        // Quit the application
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif

        SaveDataManager.Instance.SavePlayerData();
    }
}
