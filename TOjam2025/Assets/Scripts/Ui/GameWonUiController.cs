using UnityEngine;

public class GameWonUiController : MonoBehaviour
{
    [SerializeField]
    private Canvas m_canvas;

    public void ShowUi()
    {
        m_canvas.gameObject.SetActive(true);
        m_canvas.enabled = true;
    }

    public void HideUi()
    {
        m_canvas.gameObject.SetActive(false);
        m_canvas.enabled = false;
    }
}
