using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class TransitionUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nextLevelText;
    [SerializeField] private TextMeshProUGUI remainingLevelsText;

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        int totalLevels = PlayerPrefs.GetInt("TotalLevels", 8); 

        int nextLevel = currentLevel + 1;
        int remainingLevels = Mathf.Max(0, totalLevels - currentLevel);

        nextLevelText.text = nextLevel.ToString();
        remainingLevelsText.text = remainingLevels.ToString();
    }
}


