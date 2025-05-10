using UnityEngine;

public class PlayerGoal : MonoBehaviour
{
    public event System.Action PlayerReachedGoal = delegate { };

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerReachedGoal.Invoke();
        }
    }
}
