using UnityEngine;

public class GameWonGameState : IGameState
{
    private static string STATE_NAME = "GameWonGameState";
    public string StateName => STATE_NAME;

    public event System.Action RestartGameRequested = delegate { };

    private GameWonUiController m_gameWonUiController;

    public GameWonGameState()
    {
        m_gameWonUiController = GameObject.FindFirstObjectByType<GameWonUiController>();
    }

    public void OnEnter(string previous)
    {
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
