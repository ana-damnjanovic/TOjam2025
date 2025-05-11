using System;
using UnityEngine;

public class IntroCutsceneGameState : IGameState
{
    private static string STATE_NAME = "IntroCutsceneGameState";
    public string StateName => STATE_NAME;

    public event System.Action CutsceneFinished = delegate { };

    private CutsceneUiController m_cutsceneUiController;

    public IntroCutsceneGameState()
    {
        m_cutsceneUiController = GameObject.FindAnyObjectByType<CutsceneUiController>();
    }

    public void OnEnter(string previous)
    {
        m_cutsceneUiController.CutsceneAnimationFinished += OnCutsceneFinished;
        m_cutsceneUiController.ShowCutscene();
    }

    private void OnCutsceneFinished()
    {
        m_cutsceneUiController.CutsceneAnimationFinished -= OnCutsceneFinished;
        CutsceneFinished.Invoke();
    }

    public void OnExit(string next)
    {
        m_cutsceneUiController.HideCutscene();
    }

    public void OnOverride(string next)
    {

    }

    public void OnResume(string previous)
    {

    }
}
