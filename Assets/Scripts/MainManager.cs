using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text CurrentScoreText;  // Text component for displaying current score
    public Text HighScoreText;  // Text component for displaying high score
    public GameObject GameOverText;  // Text/Panel displayed when the game is over

    public Button BackToMenuButton; //Button to go back to menu

    private bool m_Started = false;  // Flag to check if the game has started
    private int m_Points;  // Current score points

    private bool m_GameOver = false;  // Flag to check if the game is over

    
    // Start is called before the first frame update
    void Start()
    {
        LoadHighScore();
        HighScoreText.text = $"Best Score : {SaveDataManager.Instance.playerName} : {SaveDataManager.Instance.bestScore}";
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        CurrentScoreText.text = $"Score : {m_Points}";
        if (m_Points > SaveDataManager.Instance.bestScore)
        {
            SaveDataManager.Instance.bestScore = m_Points;
            SaveDataManager.Instance.playerName = PlayerPrefs.GetString("CurrentPlayerName", "Player"); // Update high score holder's name
            SaveDataManager.Instance.SavePlayerData();
            UpdateHighScoreDisplay();
        }

    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        BackToMenuButton.gameObject.SetActive(true);
        UpdateHighScoreDisplay();
    }

    void LoadHighScore()
    {
        // Load high score from SaveDataManager
        SaveDataManager.Instance.LoadPlayerData();
        UpdateHighScoreDisplay();
    }

    void UpdateHighScoreDisplay()
    {
        // Update UI for score and high score
        CurrentScoreText.text = $"Score : {m_Points}";
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}
