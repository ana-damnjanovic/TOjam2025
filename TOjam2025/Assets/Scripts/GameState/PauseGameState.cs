using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseGameState : IGameState
{
    private static string STATE_NAME = "PauseGameState";
    public string StateName => STATE_NAME;

    public event System.Action ResumeRequested = delegate { };

    private PauseMenuUiController m_pauseMenuUiController;
    private PlayerInput m_playerInput;

    public PauseGameState()
    {
        m_pauseMenuUiController = GameObject.FindFirstObjectByType<PauseMenuUiController>();
        m_playerInput = GameObject.FindFirstObjectByType<PlayerInput>();
    }

    public void OnEnter(string previous)
    {
        m_playerInput.SwitchCurrentActionMap("UI");
        m_pauseMenuUiController.ResumeGameRequested += OnResumeRequested;
        m_pauseMenuUiController.ShowMenu();
    }

    private void OnResumeRequested()
    {
        ResumeRequested.Invoke();
    }

    public void OnExit(string next)
    {
        m_pauseMenuUiController.ResumeGameRequested -= OnResumeRequested;
        m_pauseMenuUiController.HideMenu();
    }

    public void OnOverride(string next)
    {
        // possible override with a controls/options menu?
    }

    public void OnResume(string previous)
    {
        
    }
}
