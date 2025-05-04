using UnityEngine;

public class GameplayGameState : IGameState
{
    private static string STATE_NAME = "GameplayGameState";
    public string StateName => STATE_NAME;

    public void OnEnter(string previous)
    {
        throw new System.NotImplementedException();
    }

    public void OnExit(string next)
    {
        throw new System.NotImplementedException();
    }

    public void OnOverride(string next)
    {
        throw new System.NotImplementedException();
    }

    public void OnResume(string previous)
    {
        throw new System.NotImplementedException();
    }
}
