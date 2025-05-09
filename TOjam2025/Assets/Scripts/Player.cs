using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputRelay m_playerInputRelay;
    private Rigidbody2D m_rigidBody;

    [SerializeField]
    private float m_speed = 10f;

    private Vector2 m_movementDirection;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    public void SetPlayerInputRelay(PlayerInputRelay inputRelay)
    {
        if (null != m_playerInputRelay)
        {
            UnsubscribeFromInputs();
        }
        m_playerInputRelay = inputRelay;
        SubscribeToInputs();
    }

    private void SubscribeToInputs()
    {
        m_playerInputRelay.MoveActionPerformed += OnMoveInputPerformed;
    }

    private void OnMoveInputPerformed(Vector2 direction)
    {
        m_movementDirection = direction;   
    }

    private void FixedUpdate()
    {
        m_rigidBody.MovePosition((Vector2)transform.position + m_movementDirection * m_speed * Time.deltaTime);
    }

    private void UnsubscribeFromInputs()
    {
        m_playerInputRelay.MoveActionPerformed -= OnMoveInputPerformed;
    }
}
