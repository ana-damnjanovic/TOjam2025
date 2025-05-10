using UnityEngine;

public class LevelTransitionGameState : IGameState
{
    private static string STATE_NAME = "LevelTransitionGameState";
    public string StateName => STATE_NAME;

    public event System.Action LevelTransitionFinished = delegate { };

    public LevelTransitionGameState()
    {
        // find the script that runs the cutscene here
    }

    public void OnEnter(string previous)
    {
        // play cutscene

        LevelTransitionFinished.Invoke();
    }

    public void OnExit(string next)
    {
        
    }

    public void OnOverride(string next)
    {
        
    }

    public void OnResume(string previous)
    {
        
    }
}
