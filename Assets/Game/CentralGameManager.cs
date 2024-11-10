using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using Udar;
using Udar.SceneManager;

public class CentralGameManager : MonoBehaviour
{
    public static CentralGameManager instance;

    // places we'll need likely access to
    private GameSettings gameSettings;
    private PlayerStats playerStats;
    private bool isPaused = false;

    [SerializeField] private SceneField openingScene;
    [SerializeField] private SceneField level1;
    [SerializeField] private SceneField level2;
    [SerializeField] private SceneField level3; 
    [SerializeField] private SceneField level4; 
    [SerializeField] private SceneField level5;
    [SerializeField] private SceneField level6;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        gameSettings = DataDictionary.GameSettings;
        playerStats = DataDictionary.PlayerStats;

        SubscribeGameEvents();
    }

    private void OnDestroy()
    {
        UnSubscribeGameEvents();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            GameEvents.GamePaused(isPaused);
        }
    }

    #region UTILITIES

    private void SubscribeGameEvents()
    {
        GameEvents.OnGameStart += OnGameStart;
        GameEvents.OnLevelEnd += LoadLevel;
    }

    private void UnSubscribeGameEvents()
    {
        GameEvents.OnLevelEnd -= LoadLevel;
        GameEvents.OnGameStart -= OnGameStart;
    }

    private void OnGameStart()
    {
        gameSettings.GameLevel = 0;
        playerStats.ResetDefaults();
        SceneManager.LoadScene(openingScene.BuildIndex);
    }

    private void LoadLevel()
    {
        gameSettings.GameLevel++;
        if (gameSettings.GameLevel <= 5) // first five floors are level 1
        {
            SceneManager.LoadScene(level1.BuildIndex);
        }
        else if (gameSettings.GameLevel <= 10) // next 5 are level 2
        {
            SceneManager.LoadScene(level2.BuildIndex);
        }
        else if (gameSettings.GameLevel <= 15)
        {
            SceneManager.LoadScene(level3.BuildIndex);
        }
        else if (gameSettings.GameLevel <= 20)
        {
            SceneManager.LoadScene(level4.BuildIndex);
        }
        else if (gameSettings.GameLevel <= 25)
        {
            SceneManager.LoadScene(level5.BuildIndex);
        }
        else // rest of game
        {
            SceneManager.LoadScene(level6.BuildIndex);
        }
    }

    #endregion
}
