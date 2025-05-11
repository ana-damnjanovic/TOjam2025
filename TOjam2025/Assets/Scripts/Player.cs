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

    private float m_currentSpeed;

    private bool m_useInvertedControls = false;

    private bool m_isJittering = false;
    private float m_jitterMin;
    private float m_jitterMax;
    private SpriteJitter m_spriteJitter;
    private SpriteRenderer m_spriteRenderer;

    private bool m_isBouncing = false;
    // time during which player can't move and is forced to bounce in the collision direction
    private float m_bounceTime = 0.5f;
    private float m_bounceTimeElapsed = 0f;

    public event System.Action LevelFailed = delegate { };

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_currentSpeed = m_baseSpeed;
        m_spriteJitter = GetComponentInChildren<SpriteJitter>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    public void EnableMovement()
    {
        m_canMove = true;
        m_isBouncing = false;
        AddListeners();
    }

    public void DisableMovement()
    {
        RemoveListeners();
        m_canMove = false;
        m_isBouncing = false;
        m_movementDirection = Vector2.zero;
        m_rigidBody.linearVelocity = Vector2.zero;
        m_rigidBody.angularVelocity = 0f;
    }

    public void InvertControls(bool isInverted)
    {
        m_useInvertedControls = isInverted;
    }

    public void ApplySpeedModifier(float modifier)
    {
        m_currentSpeed = m_baseSpeed * modifier;
    }


    public void EnableJitter(float min, float max)
    {
        m_isJittering = true;
        m_jitterMin = min;
        m_jitterMax = max;
        m_spriteJitter.StartJitter();
    }

    public void DisableJitter()
    {
        m_isJittering = false;
        m_jitterMin = 0f;
        m_jitterMax = 0f;
        m_spriteJitter.StopJitter();
    }

    public void SetPlayerInputRelay(PlayerInputRelay inputRelay)
    {
        if (null != m_playerInputRelay)
        {
            RemoveListeners();
        }
        m_playerInputRelay = inputRelay;

    }

    public void ApplyBounce(Vector2 bounceDirection)
    {
        m_isBouncing = true;
        m_canMove = false;
        m_bounceTimeElapsed = 0f;
        m_movementDirection = bounceDirection;
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
        if (m_canMove)
        {
            if (m_useInvertedControls)
            {
                m_movementDirection = new Vector2(-direction.x, -direction.y);
            }
            else
            {
                m_movementDirection = direction;
            }
            AdjustSpriteDirection();
        }
    }

    private void AdjustSpriteDirection()
    {
        if (m_movementDirection.x < 0)
        {
            // our enemy sprites faces right by default, we need to flip it
            m_spriteRenderer.flipX = true;
        }
        else if (m_movementDirection.x > 0)
        {
            m_spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        if (m_canMove)
        {
            if (m_isJittering)
            {
                //float xJitter = 0.01f;
                float xJitter = UnityEngine.Random.Range(m_jitterMin, m_jitterMax) * RandomSign();
                //float yJitter = UnityEngine.Random.Range(m_jitterMin, m_jitterMax) * RandomSign();
                float yJitter = 0.01f * RandomSign();
                Vector2 jitterOffset = new Vector2(xJitter, yJitter);
                m_rigidBody.MovePosition((Vector2)transform.position + jitterOffset + m_movementDirection * m_currentSpeed * Time.deltaTime);
            }
            else
            {
                m_rigidBody.MovePosition((Vector2)transform.position + m_movementDirection * m_currentSpeed * Time.deltaTime);
            }
        }
        else if (m_isBouncing)
        {
            m_rigidBody.MovePosition((Vector2)transform.position + m_movementDirection * m_currentSpeed * Time.deltaTime);
            m_bounceTimeElapsed += Time.deltaTime;
            if (m_bounceTimeElapsed > m_bounceTime)
            {
                m_isBouncing = false;
                m_movementDirection = Vector2.zero;
                m_rigidBody.linearVelocity = Vector2.zero;
                m_rigidBody.angularVelocity = 0f;
                m_canMove = true;
            }
        }
    }

    private float RandomSign()
    {
        return UnityEngine.Random.value < 0.5f ? 1 : -1;
    }
}
