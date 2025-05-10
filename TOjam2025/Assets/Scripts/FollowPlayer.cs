using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //camera restriction
    public Vector2 minPosition;
    public Vector2 maxPosition;

    private GameObject m_player;
    private Vector3 m_offset;
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        m_player = GameObject.FindFirstObjectByType<Player>().gameObject;
        m_offset = m_player.transform.position - this.transform.position;
        m_offset = new Vector3(m_offset.x, m_offset.y + 5f, transform.position.z);
    }

    void Update()
    {
        if (null!= m_player && m_player.activeInHierarchy)
        {
            Vector3 targetPosition = m_player.transform.position + m_offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            float clampedX = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            float clampedY = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = new Vector3(clampedX, clampedY, targetPosition.z);
        }
    }
}
