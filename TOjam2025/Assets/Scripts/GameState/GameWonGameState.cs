using UnityEngine;

public class GameWonGameState : IGameState
{
    private static string STATE_NAME = "GameWonGameState";
    public string StateName => STATE_NAME;

    public event System.Action RestartGameRequested = delegate { };

    public void OnEnter(string previous)
    {
        // TODO: show game win menu

        Debug.Log("ALL LEVELS COMPLETED!!");
    }

    public void OnExit(string next)
    {
        
    }

    public void OnOverride(string next)
    {
        // game won state probably won't have an override
    }

    public void OnResume(string previous)
    {
        
    }
}
