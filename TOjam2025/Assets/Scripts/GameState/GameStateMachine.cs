using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    private Stack<IGameState> m_activeStates;

    public GameStateMachine()
    {
        m_activeStates = new Stack<IGameState>();
    }

    public void OverrideState(IGameState nextState)
    {
        if (m_activeStates.Count > 0)
        {
            IGameState currentState = m_activeStates.Peek();
            currentState.OnOverride(nextState.StateName);
            m_activeStates.Push(nextState);
        }
        else
        {
            Debug.LogError($"no current state to override with {nextState.StateName}");
        }
    }

    public void ResumePreviousState()
    {
        if (m_activeStates.Count > 1)
        {
            IGameState previousState = m_activeStates.Pop();
            IGameState currentState = m_activeStates.Peek();
            currentState.OnResume(previousState.StateName);
        }
        else
        {
            Debug.LogError($"no state to resume");
        }
    }

    public void ChangeState(IGameState nextState)
    {
        IGameState currentState = null;
        if (m_activeStates.Count > 0)
        {
            currentState = m_activeStates.Peek();
        }
        if (null != currentState)
        {
            if (currentState != nextState)
            {
                currentState.OnExit(nextState.StateName);
                m_activeStates.Pop();
                m_activeStates.Push(nextState);
                nextState.OnEnter(currentState.StateName);
            }
            else
            {
                Debug.LogError($"already in state {nextState.StateName}");
            }
        }
        else
        {
            m_activeStates.Push(nextState);
            string stateName = "";
            if (null != currentState)
            {
                stateName = currentState.StateName;
            }
            nextState.OnEnter(stateName);
        }
    }
}
