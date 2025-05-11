using System.Collections;
using UnityEngine;

public class CutsceneUiController : MonoBehaviour
{
    [SerializeField]
    private Canvas m_canvas;

    [SerializeField]
    private float m_animationTime = 10f;

    public event System.Action CutsceneAnimationFinished = delegate { };

    public void ShowCutscene()
    {
        m_canvas.enabled = true;
        StartCoroutine(AnimationTimer());
    }

    public void HideCutscene()
    {
        m_canvas.enabled = false;
    }

    private IEnumerator AnimationTimer()
    {
        float timeElapsed = 0f;
        while (timeElapsed < m_animationTime)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        CutsceneAnimationFinished.Invoke();
    }
}
