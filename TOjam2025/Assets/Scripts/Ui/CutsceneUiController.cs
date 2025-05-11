using System.Collections;
using UnityEngine;

public class CutsceneUiController : MonoBehaviour
{
    [SerializeField]
    private Canvas m_canvas;

    [SerializeField]
    private CutscenePlay m_cutscenePlayScript;

    public event System.Action CutsceneAnimationFinished = delegate { };

    public void ShowCutscene()
    {
        m_canvas.enabled = true;
        m_canvas.gameObject.SetActive(true);
        m_cutscenePlayScript.CutsceneFinished += OnCutsceneFinished;
        m_cutscenePlayScript.PlayCutscene();
    }

    public void HideCutscene()
    {
        m_canvas.gameObject.SetActive(false);
        m_canvas.enabled = false;
    }

    private void OnCutsceneFinished()
    {
        m_cutscenePlayScript.CutsceneFinished -= OnCutsceneFinished;
        CutsceneAnimationFinished.Invoke();
    }
}
