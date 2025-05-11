using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class TransitionUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nextLevelText;
    [SerializeField] private TextMeshProUGUI remainingLevelsText;

    [SerializeField]
    private Canvas m_canvas;
 
    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private float m_displayTime = 2.5f;

    [SerializeField]
    private TextMeshProUGUI m_levelInstructionsText;

    private float m_displayTimeElapsed = 0f;

    private bool m_animationStarted = false;

    public event System.Action TransitionFinished = delegate { };


    public void ShowUi(int level, int maxLevel, string levelText)
    {
        int remainingLevels = Mathf.Max(0, maxLevel - level);

        nextLevelText.text = level.ToString();
        remainingLevelsText.text = remainingLevels.ToString();

        m_levelInstructionsText.SetText(levelText);

        m_canvas.gameObject.SetActive(true);
        m_canvas.enabled = true;

        m_displayTimeElapsed = 0f;
        m_animator.Play("Transition Anim");
        m_animationStarted = true;
    }

    public void HideUi()
    {
        m_canvas.gameObject.SetActive(false);
        m_canvas.enabled = false;
        m_animationStarted = false;
    }

    private void Update()
    {
       
       if (m_animationStarted )
        {
            if (m_displayTimeElapsed >= m_displayTime && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                TransitionFinished.Invoke();
            }
            else
            {
                m_displayTimeElapsed += Time.deltaTime;
            }
        }
    }
}


