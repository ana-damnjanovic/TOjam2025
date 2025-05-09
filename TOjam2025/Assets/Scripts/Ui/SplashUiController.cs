using UnityEngine;

public class SplashUiController : MonoBehaviour
{
    [SerializeField]
    private Canvas m_canvas;

    public void ShowSplash()
    {
        m_canvas.enabled = true;
    }

    public void HideSplash()
    {
        m_canvas.enabled = false;
    }
}
