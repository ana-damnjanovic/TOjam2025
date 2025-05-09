using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayGameState : IGameState
{
    private static string STATE_NAME = "GameplayGameState";
    public string StateName => STATE_NAME;

    public event System.Action GameOverRequested = delegate { };

    public event System.Action PauseRequested = delegate { };

    private PlayerInputController m_playerInputController;

    public void OnEnter(string previous)
    {
        // TODO: spawn player, set up and start the level
        // TODO: listen for game over or pause and throw corresponding events 


        //TODO: this will come from the spawned player
        m_playerInputController = GameObject.FindFirstObjectByType<PlayerInputController>();
        m_playerInputController.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        m_playerInputController.PauseActionPerformed += OnPauseRequested;
        
    }

    private void OnPauseRequested()
    {
        PauseRequested.Invoke();
    }

    public void OnExit(string next)
    {
        // TODO: unsubscribe from player events

        m_playerInputController.PauseActionPerformed -= OnPauseRequested;
    }

    public void OnOverride(string next)
    {
        m_playerInputController.PauseActionPerformed -= OnPauseRequested;
    }

    public void OnResume(string previous)
    {
        m_playerInputController.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        m_playerInputController.PauseActionPerformed += OnPauseRequested;
    }
}
