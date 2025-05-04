using UnityEngine;

public class GameOverGameState : IGameState
{
    private static string STATE_NAME = "GameOverGameState";
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
