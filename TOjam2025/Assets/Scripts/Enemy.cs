using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_ouchSound;

    [SerializeField]
    private AudioSource m_audioSource;

    [SerializeField]
    private float m_movementSpeed = 3f;

    [SerializeField]
    private Rigidbody2D m_rigidBody;

    private Vector2 m_movementDirection;

    private SpriteRenderer m_spriteRenderer;

    private void Awake()
    {
        m_movementDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        AdjustSpriteDirection();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // bounce in the opposite direction of the hit
        m_movementDirection = (this.transform.position - collision.gameObject.transform.position).normalized;
        AdjustSpriteDirection();

        if (collision.gameObject.CompareTag("Player")){
            m_audioSource.PlayOneShot(m_ouchSound);
            collision.gameObject.GetComponent<Player>().ApplyBounce(-m_movementDirection);
        }
    }

    private void AdjustSpriteDirection()
    {
        if (m_movementDirection.x > 0)
        {
            // all our enemy sprites face left by default, we need to flip it
            m_spriteRenderer.flipX = true;
        }
        else
        {
            m_spriteRenderer.flipX = false;
        }
    }

    private void Update()
    {
        m_rigidBody.MovePosition((Vector2)transform.position + m_movementDirection * m_movementSpeed * Time.deltaTime);
    }
}
