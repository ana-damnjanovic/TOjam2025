using UnityEngine;

public class PlayerInputController : MonoBehaviour
{

    public event System.Action PauseActionPerformed = delegate { };

    public void OnMove()
    {
        Debug.Log("move");
    }

    public void OnPause()
    {
        PauseActionPerformed.Invoke();
    }
}
