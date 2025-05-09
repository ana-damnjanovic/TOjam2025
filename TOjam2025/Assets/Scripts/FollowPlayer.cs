using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject m_player;
    private Vector3 m_offset;
    void Start()
    {
        m_player = GameObject.FindFirstObjectByType<Player>().gameObject;
        m_offset = m_player.transform.position - this.transform.position;
        m_offset = new Vector3(m_offset.x, m_offset.y, transform.position.z);
    }

    void Update()
    {
        if (null!= m_player && m_player.activeInHierarchy)
        {
            transform.position = m_player.transform.position + m_offset;
        }
    }
}
