using System.Collections;
using UnityEngine;

public class SplashUiController : MonoBehaviour
{
    [SerializeField]
    private Canvas m_canvas;

    [SerializeField]
    private float m_animationTime = 1f;

    public event System.Action SplashAnimationFinished = delegate { };

    public void ShowSplash()
    {
        m_canvas.enabled = true;
        StartCoroutine(AnimationTimer());
    }

    public void HideSplash()
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

        SplashAnimationFinished.Invoke();
    }
}
