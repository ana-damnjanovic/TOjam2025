using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayGameState : IGameState
{
    private static string STATE_NAME = "GameplayGameState";
    public string StateName => STATE_NAME;

    public int LevelTransitionRequested { get; internal set; }

    public event System.Action GameWonRequested = delegate { };

    public event System.Action PauseRequested = delegate { };

    public event System.Action NextLevelRequested = delegate { };

    private PlayerInput m_playerInput;
    private PlayerInputRelay m_playerInputRelay;
    private Player m_player;
    private LevelManager m_levelManager;

    public GameplayGameState(PlayerInput playerInput, PlayerInputRelay playerInputRelay)
    {
        m_playerInput = playerInput;
        m_playerInputRelay = playerInputRelay;
        m_levelManager = GameObject.FindFirstObjectByType<LevelManager>();
        m_player = GameObject.FindAnyObjectByType<Player>();
        m_player.SetPlayerInputRelay(m_playerInputRelay);
    }

    public void OnEnter(string previous)
    {
        m_levelManager.Initialize();

        m_playerInput.SwitchCurrentActionMap("Player");

        AddListeners();
    }

    private void AddListeners()
    {
        m_levelManager.LevelSucceeded += OnLevelSucceeded;
        m_levelManager.AllLevelsWon += OnAllLevelsWon;
        m_playerInputRelay.PauseActionPerformed += OnPauseRequested;
    }

    private void OnAllLevelsWon()
    {
        GameWonRequested.Invoke();
    }

    private void RemoveListeners()
    {
        m_levelManager.LevelSucceeded -= OnLevelSucceeded;
        m_levelManager.AllLevelsWon -= OnAllLevelsWon;
        m_playerInputRelay.PauseActionPerformed -= OnPauseRequested;
    }

    private void OnLevelSucceeded()
    {
        NextLevelRequested.Invoke();
    }

    private void OnPauseRequested()
    {
        PauseRequested.Invoke();
    }

    public void OnExit(string next)
    {
        RemoveListeners();
    }

    public void OnOverride(string next)
    {
        RemoveListeners();
    }

    public void OnResume(string previous)
    {
        m_playerInputRelay.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        AddListeners();
    }
}
