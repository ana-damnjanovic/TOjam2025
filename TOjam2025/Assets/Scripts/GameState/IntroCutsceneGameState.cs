using UnityEngine;

public class IntroCutsceneGameState : IGameState
{
    private static string STATE_NAME = "IntroCutsceneGameState";
    public string StateName => STATE_NAME;

    public event System.Action CutsceneFinished = delegate { };

    public IntroCutsceneGameState()
    {
        // find the script that controls the cutscene here
    }

    public void OnEnter(string previous)
    {
        // play cutscene

        CutsceneFinished.Invoke();
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
