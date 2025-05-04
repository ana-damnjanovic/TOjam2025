using UnityEngine;

public class GameOverGameState : IGameState
{
    private static string STATE_NAME = "GameOverGameState";
    public string StateName => STATE_NAME;

    public event System.Action RestartGameRequested = delegate { };

    public void OnEnter(string previous)
    {
        // TODO: show game over menu
        // TODO: clean up, delete player and tell game manager to reset level progress
    }

    public void OnExit(string next)
    {
        
    }

    public void OnOverride(string next)
    {
        // game over state probably won't have an override
    }

    public void OnResume(string previous)
    {
        
    }
}
