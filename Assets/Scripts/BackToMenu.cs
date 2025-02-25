using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public Button BackToMenuButton;
    
    public void BackToMenuFunction()
    {
        SceneManager.LoadScene(0);
    }
}
