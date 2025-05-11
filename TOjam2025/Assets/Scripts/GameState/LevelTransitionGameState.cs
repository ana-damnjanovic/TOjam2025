using System;
using UnityEngine;

public class LevelTransitionGameState : IGameState
{
    private static string STATE_NAME = "LevelTransitionGameState";
    public string StateName => STATE_NAME;

    public event System.Action LevelTransitionFinished = delegate { };

    private TransitionUIController m_transitionUiController;
    private LevelManager m_levelManager;

    public LevelTransitionGameState()
    {
        // find the script that runs the cutscene here
        m_transitionUiController = GameObject.FindFirstObjectByType<TransitionUIController>();
        m_levelManager = GameObject.FindFirstObjectByType<LevelManager>();
    }

    public void OnEnter(string previous)
    {
        m_transitionUiController.ShowUi(m_levelManager.GetCurrentLevel, m_levelManager.GetMaxLevel);
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
