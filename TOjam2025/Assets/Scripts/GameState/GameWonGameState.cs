using System;
using UnityEngine;

public class GameWonGameState : IGameState
{
    private static string STATE_NAME = "GameWonGameState";
    public string StateName => STATE_NAME;

    public event System.Action MainMenuRequested = delegate { };

    private GameWonUiController m_gameWonUiController;
    private MinimapController m_minimapController;


    public GameWonGameState()
    {
        m_gameWonUiController = GameObject.FindFirstObjectByType<GameWonUiController>();

        m_minimapController = GameObject.FindFirstObjectByType<MinimapController>();
    }

    public void OnEnter(string previous)
    {
        // Hack to shut minimap off so the game over win screen can display
        m_minimapController.DisableMinimap();
        m_minimapController.StopBounce();

        m_gameWonUiController.MainMenuRequested += OnMainMenuRequested;
        m_gameWonUiController.ShowUi();
    }

    private void OnMainMenuRequested()
    {
        m_gameWonUiController.MainMenuRequested -= OnMainMenuRequested;
        MainMenuRequested.Invoke();
    }

    public void OnExit(string next)
    {
        m_gameWonUiController.HideUi();
    }

    public void OnOverride(string next)
    {
        // game won state probably won't have an override
    }

    public void OnResume(string previous)
    {
        
    }
}
