using System;
using UnityEngine;

public class LevelTransitionGameState : IGameState
{
    private static string STATE_NAME = "LevelTransitionGameState";
    public string StateName => STATE_NAME;

    public event System.Action LevelTransitionFinished = delegate { };

    private TransitionUIController m_transitionUiController;

    public LevelTransitionGameState()
    {
        // find the script that runs the cutscene here
        m_transitionUiController = GameObject.FindFirstObjectByType<TransitionUIController>();
    }

    public void OnEnter(string previous)
    {
        m_transitionUiController.ShowUi();
        m_transitionUiController.TransitionFinished += OnTransitionFinished;
    }

    private void OnTransitionFinished()
    {
        m_transitionUiController.HideUi();
        LevelTransitionFinished.Invoke();
    }

    public void OnExit(string next)
    {
        m_transitionUiController.TransitionFinished -= OnTransitionFinished;
    }

    public void OnOverride(string next)
    {
        
    }

    public void OnResume(string previous)
    {
        
    }
}
