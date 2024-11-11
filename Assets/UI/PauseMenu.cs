using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject root;

    private static PauseMenu instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        GameEvents.OnGamePaused += OnPause;
        OnPause(false);
    }

    private void OnDestroy()
    {
        GameEvents.OnGamePaused -= OnPause;
    }

    private void OnPause(bool _paused)
    {
        if (_paused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
        root.SetActive(_paused);
    }

    public void OnExitToMenu()
    {
        GameEvents.GamePaused(false);
        CentralGameManager.Paused = false;
        GameEvents.ExitToMainMenu();
    }

    public void OnKYS()
    {
        GameEvents.GamePaused(false);
        CentralGameManager.Paused = false;
        GameEvents.PlayerRespawn();
    }

    public void OnContinue()
    {
        CentralGameManager.Paused = false;
        GameEvents.GamePaused(false);
    }
}
