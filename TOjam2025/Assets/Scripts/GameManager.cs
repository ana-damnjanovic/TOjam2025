using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private SceneLoader m_sceneLoader;
    private SplashUiController m_splashUiController;

    private GameStateMachine m_gameStateMachine;
    private MainMenuGameState m_mainMenuState;
    private IntroCutsceneGameState m_introCutsceneState;
    private GameplayGameState m_gameplayState;
    private PauseGameState m_pauseState;
    private LevelTransitionGameState m_transitionState;
    private GameWonGameState m_gameWonState;

    private PlayerInput m_playerInput;
    private PlayerInputRelay m_playerInputRelay;

    private bool m_sceneLoadCompleted = false;
    private bool m_splashAnimationFinished = false;

    private void Awake()
    {
        m_splashUiController = FindAnyObjectByType<SplashUiController>();

        m_playerInput = FindFirstObjectByType<PlayerInput>();
        m_playerInputRelay = m_playerInput.GetComponent<PlayerInputRelay>();

        m_sceneLoader = FindAnyObjectByType<SceneLoader>();
        m_sceneLoader.SceneLoadCompleted += OnSceneLoadCompleted;
        m_sceneLoader.Initialize();

        m_splashUiController.SplashAnimationFinished += OnSplashAnimationFinished;
        m_splashUiController.ShowSplash();
    }

    private void OnSplashAnimationFinished()
    {
        m_splashAnimationFinished = true;
        m_splashUiController.SplashAnimationFinished -= OnSplashAnimationFinished;
        m_splashUiController.HideSplash();

        if (m_sceneLoadCompleted)
        {
            InitializeGameStates();
        }
    }

    private void OnSceneLoadCompleted()
    {
        m_sceneLoadCompleted = true;
        m_sceneLoader.SceneLoadCompleted -= OnSceneLoadCompleted;

        if (m_splashAnimationFinished)
        {
            InitializeGameStates();
        }
    }

    private void InitializeGameStates()
    {
        m_gameStateMachine = new GameStateMachine();

        m_mainMenuState = new MainMenuGameState();
        m_mainMenuState.PlayGameRequested += OnPlayGameRequested;

        m_introCutsceneState = new IntroCutsceneGameState();
        m_introCutsceneState.CutsceneFinished += OnIntroCutsceneFinished;

        m_gameplayState = new GameplayGameState(m_playerInput, m_playerInputRelay);
        m_gameplayState.GameWonRequested += OnGameWonRequested;
        m_gameplayState.PauseRequested += OnPauseRequested;
        m_gameplayState.NextLevelRequested += OnNextLevelRequested;

        m_pauseState = new PauseGameState(m_playerInput);
        m_pauseState.ResumeRequested += OnResumeRequested;

        m_transitionState = new LevelTransitionGameState();
        m_transitionState.LevelTransitionFinished += OnTransitionFinished;

        m_gameWonState = new GameWonGameState();
        m_gameWonState.MainMenuRequested += OnMainMenuRequestedAfterWin;

        m_gameStateMachine.ChangeState(m_mainMenuState);
    }

    private void OnMainMenuRequestedAfterWin()
    {
        m_gameplayState.ResetLevels();
        m_gameStateMachine.ChangeState(m_mainMenuState);
    }

    private void OnIntroCutsceneFinished()
    {
        m_gameStateMachine.ChangeState(m_transitionState);
    }

    private void OnNextLevelRequested()
    {
        m_gameStateMachine.ChangeState(m_transitionState);
    }

    private void OnTransitionFinished()
    {
        m_gameStateMachine.ChangeState(m_gameplayState);
    }

    private void OnPlayGameRequested()
    {
        m_gameStateMachine.ChangeState(m_introCutsceneState);
    }

    private void OnResumeRequested()
    {
        m_gameStateMachine.ResumePreviousState();
    }

    private void OnPauseRequested()
    {
        //m_gameStateMachine.OverrideState(m_pauseState);
    }

    private void OnGameWonRequested()
    {
        m_gameStateMachine.ChangeState(m_gameWonState);
    }
}
