using UnityEngine;

public class GameWonGameState : IGameState
{
    private static string STATE_NAME = "GameWonGameState";
    public string StateName => STATE_NAME;

    public event System.Action RestartGameRequested = delegate { };

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

        m_gameWonUiController.ShowUi();
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
