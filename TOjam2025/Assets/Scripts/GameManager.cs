using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SceneLoader m_sceneLoader;

    private GameStateMachine m_gameStateMachine;
    private MainMenuGameState m_mainMenuState;
    private GameplayGameState m_gameplayState;
    private PauseGameState m_pauseState;
    private GameOverGameState m_gameOverGameState;

    private void Awake()
    {
        m_sceneLoader = FindAnyObjectByType<SceneLoader>();
        m_sceneLoader.SceneLoadCompleted += OnSceneLoadCompleted;
        m_sceneLoader.Initialize();
    }

    private void OnSceneLoadCompleted()
    {
        m_sceneLoader.SceneLoadCompleted -= OnSceneLoadCompleted;
        InitializeGameStates();
    }

    private void InitializeGameStates()
    {
        m_gameStateMachine = new GameStateMachine();

        m_mainMenuState = new MainMenuGameState();
        m_mainMenuState.PlayGameRequested += OnPlayGameRequested;

        m_gameplayState = new GameplayGameState();
        m_gameplayState.GameOverRequested += OnGameOverRequested;
        m_gameplayState.PauseRequested += OnPauseRequested;

        m_pauseState = new PauseGameState();
        m_pauseState.ResumeRequested += OnResumeRequested;

        m_gameOverGameState = new GameOverGameState();
        m_gameOverGameState.RestartGameRequested += OnPlayGameRequested;

        m_gameStateMachine.ChangeState(m_mainMenuState);
    }

    private void OnPlayGameRequested()
    {
        m_gameStateMachine.ChangeState(m_gameplayState);
    }

    private void OnResumeRequested()
    {
        m_gameStateMachine.ResumePreviousState();
    }

    private void OnPauseRequested()
    {
        m_gameStateMachine.OverrideState(m_pauseState);
    }

    private void OnGameOverRequested()
    {
        m_gameStateMachine.ChangeState(m_gameOverGameState);
    }
}
