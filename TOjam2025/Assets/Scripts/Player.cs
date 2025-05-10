using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputRelay m_playerInputRelay;
    private Rigidbody2D m_rigidBody;

    [SerializeField]
    private float m_baseSpeed = 10f;

    [SerializeField]
    private FallDetector m_fallDetector;

    private Vector2 m_movementDirection;

    private bool m_canMove = false;

    public event System.Action LevelFailed = delegate { };

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }
    
    public void EnableMovement()
    {
        m_canMove = true;
        AddListeners();
    }

    public void DisableMovement()
    {
        RemoveListeners();
        m_canMove = false;
    }

    public void SetPlayerInputRelay(PlayerInputRelay inputRelay)
    {
        if (null != m_playerInputRelay)
        {
            RemoveListeners();
        }
        m_playerInputRelay = inputRelay;

    }

    private void AddListeners()
    {
        m_playerInputRelay.MoveActionPerformed += OnMoveInputPerformed;
        m_fallDetector.FallDetected += OnFallDetected;
    }
    private void RemoveListeners()
    {
        m_playerInputRelay.MoveActionPerformed -= OnMoveInputPerformed;
        m_fallDetector.FallDetected -= OnFallDetected;
    }

    private void OnFallDetected()
    {
        LevelFailed.Invoke();
    }

    private void OnMoveInputPerformed(Vector2 direction)
    {
        m_movementDirection = direction;   
    }

    private void FixedUpdate()
    {
        if (m_canMove)
        {
            m_rigidBody.MovePosition((Vector2)transform.position + m_movementDirection * m_baseSpeed * Time.deltaTime);
        }
    }

}
