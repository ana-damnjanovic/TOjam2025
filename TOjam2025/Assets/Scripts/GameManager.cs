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
        m_gameplayState = new GameplayGameState();
        m_pauseState = new PauseGameState();
        m_gameOverGameState = new GameOverGameState();

        m_gameStateMachine.ChangeState(m_mainMenuState);
    }
}
