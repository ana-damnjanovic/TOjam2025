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
        Vector2 bounceDirection = (this.transform.position - collision.gameObject.transform.position).normalized;

        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    Vector2 wallNormal = collision.GetContact(0).normal;
        //    if (wallNormal.x != 0)
        //    {
        //        // left or right wall
        //        m_movementDirection = new Vector2(-m_movementDirection.x, m_movementDirection.y);
        //    }
        //    else
        //    {
        //        m_movementDirection = new Vector2(m_movementDirection.x, -m_movementDirection.y);
        //    }
        //}
        if (collision.gameObject.CompareTag("Player")){
            m_audioSource.PlayOneShot(m_ouchSound);
            collision.gameObject.GetComponent<Player>().ApplyBounce(-m_movementDirection);
            m_movementDirection = bounceDirection;
        }
        else
        {
            m_movementDirection = bounceDirection;
        }

        AdjustSpriteDirection();
    }

    private void AdjustSpriteDirection()
    {
        if (m_movementDirection.x > 0)
        {
            // all our enemy sprites face left by default, we need to flip it
            m_spriteRenderer.flipX = true;
        }
        else if (m_movementDirection.x < 0)
        {
            m_spriteRenderer.flipX = false;
        }
    }

    private void Update()
    {
        m_rigidBody.MovePosition((Vector2)transform.position + m_movementDirection * m_movementSpeed * Time.deltaTime);
    }
}
