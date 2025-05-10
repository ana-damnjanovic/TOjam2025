using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public event System.Action FallDetected = delegate { };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            FallDetected.Invoke();
        }
    }
}
