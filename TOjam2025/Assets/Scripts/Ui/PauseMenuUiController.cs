using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuUiController : MonoBehaviour
{
    public event System.Action ResumeGameRequested = delegate { };

    [SerializeField]
    private Canvas m_canvas;

    [SerializeField]
    private Button m_resumeButton;

    [SerializeField]
    private InputActionReference m_pauseAction;

    public void ShowMenu()
    {
        m_canvas.enabled = true;
        m_resumeButton.onClick.AddListener(OnResumeButtonClicked);
        m_pauseAction.action.performed += OnResumeActionPerformed;
    }

    private void OnResumeActionPerformed(InputAction.CallbackContext obj)
    {
        ResumeGameRequested.Invoke();
    }

    private void OnResumeButtonClicked()
    {
        ResumeGameRequested.Invoke();
    }

    public void HideMenu()
    {
        m_canvas.enabled = false;
        m_pauseAction.action.performed -= OnResumeActionPerformed;
        m_resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
    }
}
