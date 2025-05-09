using System;
using UnityEngine;

public class MainMenuGameState : IGameState
{
    private static string STATE_NAME = "MainMenuGameState";
    public string StateName => STATE_NAME;

    public event System.Action PlayGameRequested = delegate { };

    private MainMenuUiController m_mainMenuUiController;

    public MainMenuGameState()
    {
        m_mainMenuUiController = GameObject.FindFirstObjectByType<MainMenuUiController>();
    }

    public void OnEnter(string previous)
    {
        m_mainMenuUiController.ShowMenu();
        m_mainMenuUiController.PlayGameClicked += OnPlayGameClicked;
    }

    private void OnPlayGameClicked()
    {
        PlayGameRequested.Invoke();
    }

    public void OnExit(string next)
    {
        m_mainMenuUiController.PlayGameClicked -= OnPlayGameClicked;
        m_mainMenuUiController.HideMenu();
    }

    public void OnOverride(string next)
    {
        // main menu will not be overridden/resumed
    }

    public void OnResume(string previous)
    {
        // main menu will not be overridden/resumed
    }
}
