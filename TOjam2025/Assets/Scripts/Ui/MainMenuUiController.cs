using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUiController : MonoBehaviour
{
    public event System.Action PlayGameClicked = delegate { };

    [SerializeField]
    private Canvas m_canvas;

    [SerializeField]
    private Button m_playGameButton;

    public void ShowMenu()
    {
        m_canvas.enabled = true;
        m_playGameButton.onClick.AddListener(OnPlayButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        PlayGameClicked.Invoke();
    }

    public void HideMenu()
    {
        m_canvas.enabled = false;
        m_playGameButton.onClick.RemoveListener(OnPlayButtonClicked);
    }
}
