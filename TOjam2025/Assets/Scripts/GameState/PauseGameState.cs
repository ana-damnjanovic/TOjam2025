using UnityEngine;

public class PauseGameState : IGameState
{
    private static string STATE_NAME = "PauseGameState";
    public string StateName => STATE_NAME;

    public event System.Action ResumeRequested = delegate { };

    public void OnEnter(string previous)
    {
        // TODO: show pause menu and listen for its button presses
    }

    public void OnExit(string next)
    {
        // TODO: unsubscribe from pause menu
    }

    public void OnOverride(string next)
    {
        // possible override with a controls/options menu?
    }

    public void OnResume(string previous)
    {
        
    }
}
