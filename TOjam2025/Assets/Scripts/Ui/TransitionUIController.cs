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
    private Animation m_animation;

    [SerializeField]
    private Animator m_animator;

    private bool m_animationStarted = false;

    public event System.Action TransitionFinished = delegate { };

    private void Start()
    {

    }

    public void ShowUi(int level, int maxLevel)
    {
        int remainingLevels = Mathf.Max(0, maxLevel - level);

        nextLevelText.text = level.ToString();
        remainingLevelsText.text = remainingLevels.ToString();

        m_canvas.gameObject.SetActive(true);
        m_canvas.enabled = true;

        m_animator.Play("Transition Anim");
        m_animationStarted = true;
    }

    public void HideUi()
    {
        m_canvas.gameObject.SetActive(false);
        m_canvas.enabled = false;
    }

    private void Update()
    {
       if (m_animationStarted && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            TransitionFinished.Invoke();
        }
    }
}


