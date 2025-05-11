using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public event System.Action FallDetected = delegate { };

    [SerializeField]
    private AudioClip m_fallSound;

    [SerializeField]
    private AudioSource m_audioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            //m_audioSource.PlayOneShot(m_fallSound);
            FallDetected.Invoke();
        }
    }
}
