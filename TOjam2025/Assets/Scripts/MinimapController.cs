using UnityEngine;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour
{
    [SerializeField]
    private Camera m_secondaryCamera;

    [SerializeField]
    private Canvas m_canvas;

    [SerializeField]
    private Image m_minimapImage;

    [SerializeField]
    private float m_minimapSpeed = 25f;

    private float m_xMin;

    private float m_xMax;

    private float m_yMin;

    private float m_yMax;

    private bool m_isBouncing = false;

    private Vector2 m_movementDir;
    private Vector2 m_minimapOriginalPos;
    private RectTransform m_minimapRectTransform;

    private void Awake()
    {
        m_minimapRectTransform = m_minimapImage.GetComponent<RectTransform>();
    }

    public void EnableMinimap()
    {
        m_secondaryCamera.gameObject.SetActive(true);
        m_canvas.enabled = true;
    }

    public void DisableMinimap()
    {
        m_secondaryCamera.gameObject.SetActive(false);
        m_canvas.enabled = false;
    }

    public void StartBounce()
    {
        SetMinMaxCoords();
        m_minimapOriginalPos = m_minimapRectTransform.position;
        m_movementDir = new Vector2(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        m_isBouncing = true;
    }

    public void StopBounce()
    {
        m_isBouncing = false;
        m_minimapRectTransform.position = m_minimapOriginalPos;
    }

    private void SetMinMaxCoords()
    {
        m_xMin = 10f + m_minimapRectTransform.rect.width / 2f;
        m_yMin = 10f + m_minimapRectTransform.rect.height / 2f;
        m_xMax = Screen.width -10f - m_minimapRectTransform.rect.width / 2f;
        m_yMax = Screen.height -10f - m_minimapRectTransform.rect.height / 2f;
    }

    private void Update()
    {
        if (m_isBouncing)
        {
            Vector2 posChange = m_movementDir * m_minimapSpeed * Time.deltaTime;
            m_minimapRectTransform.position = new Vector2(m_minimapRectTransform.position.x + posChange.x, m_minimapRectTransform.position.y + posChange.y);
            if (m_minimapRectTransform.position.x <= m_xMin || m_minimapRectTransform.position.x >= m_xMax)
            {
                m_movementDir = new Vector2(-m_movementDir.x, m_movementDir.y);
            }
            else if (m_minimapRectTransform.position.y <= m_yMin || m_minimapRectTransform.position.y >= m_yMax)
            {
                m_movementDir = new Vector2(m_movementDir.x, -m_movementDir.y);
            }
        }
    }
}
