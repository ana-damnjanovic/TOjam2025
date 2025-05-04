using UnityEngine;

public class GameplayGameState : IGameState
{
    private static string STATE_NAME = "GameplayGameState";
    public string StateName => STATE_NAME;

    public event System.Action GameOverRequested = delegate { };

    public event System.Action PauseRequested = delegate { };

    public void OnEnter(string previous)
    {
        // TODO: spawn player, set up and start the level
        // TODO: listen for game over or pause and throw corresponding events 
    }

    public void OnExit(string next)
    {
        // TODO: unsubscribe from player events
    }

    public void OnOverride(string next)
    {
        // pause time
    }

    public void OnResume(string previous)
    {
        // resume time
    }
}
