using UnityEngine;

public class MainMenuGameState : IGameState
{
    private static string STATE_NAME = "MainMenuGameState";
    public string StateName => STATE_NAME;

    public event System.Action PlayGameRequested = delegate { };

    public void OnEnter(string previous)
    {
        // TODO: listen to main menu buttons and throw PlayGameRequested event when the play button gets pressed
    }

    public void OnExit(string next)
    {
        // TODO: unsubscribe from menu
    }

    public void OnOverride(string next)
    {
        // main menu will not be overridden/resumed
    }

    public void OnResume(string previous)
    {
        // main menu will not be overridden/resumed
    }
}
