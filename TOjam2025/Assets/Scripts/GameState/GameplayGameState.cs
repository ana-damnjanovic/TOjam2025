using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayGameState : IGameState
{
    private static string STATE_NAME = "GameplayGameState";
    public string StateName => STATE_NAME;

    public event System.Action GameOverRequested = delegate { };

    public event System.Action PauseRequested = delegate { };

    private PlayerInput m_playerInput;
    private PlayerInputRelay m_playerInputRelay;
    private Player m_player;

    public GameplayGameState(PlayerInput playerInput, PlayerInputRelay playerInputRelay)
    {
        m_playerInput = playerInput;
        m_playerInputRelay = playerInputRelay;
    }

    public void OnEnter(string previous)
    {
        // TODO: spawn player, set up and start the level
        // TODO: listen for game over or pause and throw corresponding events 
        m_player = GameObject.FindAnyObjectByType<Player>();
        m_player.SetPlayerInputRelay(m_playerInputRelay);

        m_playerInput.SwitchCurrentActionMap("Player");
        m_playerInputRelay.PauseActionPerformed += OnPauseRequested;
        
    }

    private void OnPauseRequested()
    {
        PauseRequested.Invoke();
    }

    public void OnExit(string next)
    {
        // TODO: unsubscribe from player events

        m_playerInputRelay.PauseActionPerformed -= OnPauseRequested;
    }

    public void OnOverride(string next)
    {
        m_playerInputRelay.PauseActionPerformed -= OnPauseRequested;
    }

    public void OnResume(string previous)
    {
        m_playerInputRelay.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        m_playerInputRelay.PauseActionPerformed += OnPauseRequested;
    }
}
