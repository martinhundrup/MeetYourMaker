using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private GameSettings gameSettings;

    private void Awake()
    {
        gameSettings = DataDictionary.GameSettings;
    }

    public void StartGame()
    {
        gameSettings.GameLevel = 0;
        SceneManager.LoadScene("Game");
    }
}
