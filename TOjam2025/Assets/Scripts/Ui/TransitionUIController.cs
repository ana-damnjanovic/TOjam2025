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
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        int totalLevels = PlayerPrefs.GetInt("TotalLevels", 8); 

        int nextLevel = currentLevel + 1;
        int remainingLevels = Mathf.Max(0, totalLevels - currentLevel);

        nextLevelText.text = nextLevel.ToString();
        remainingLevelsText.text = remainingLevels.ToString();
    }

    public void ShowUi()
    {
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


