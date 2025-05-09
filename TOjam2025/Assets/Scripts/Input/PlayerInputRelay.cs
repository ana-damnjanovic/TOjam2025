using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputRelay : MonoBehaviour
{

    public event System.Action PauseActionPerformed = delegate { };
    public event System.Action<Vector2> MoveActionPerformed = delegate { };

    public void OnMove(InputValue value)
    {
        Vector2 moveDirection = value.Get<Vector2>();
        MoveActionPerformed.Invoke(moveDirection);
    }

    public void OnPause()
    {
        PauseActionPerformed.Invoke();
    }
}
