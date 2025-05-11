using UnityEngine;
using UnityEngine.UI;

public class GameWonUiController : MonoBehaviour
{
    [SerializeField]
    private Canvas m_canvas;

    [SerializeField]
    private Button m_mainMenuButton;

    public event System.Action MainMenuRequested = delegate { };

    public void ShowUi()
    {
        m_canvas.gameObject.SetActive(true);
        m_canvas.enabled = true;
        m_mainMenuButton.onClick.AddListener(OnMainMenuClicked);
    }

    public void HideUi()
    {
        m_canvas.gameObject.SetActive(false);
        m_canvas.enabled = false;
    }
    private void OnMainMenuClicked()
    {
        m_mainMenuButton.onClick.RemoveListener(OnMainMenuClicked);
        MainMenuRequested.Invoke();
    }
}
