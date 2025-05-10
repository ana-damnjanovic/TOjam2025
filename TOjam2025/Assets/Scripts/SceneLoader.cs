using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string m_mainMenuSceneName;

    [SerializeField]
    private string m_gameplaySceneName;

    [SerializeField]
    private string m_pauseMenuSceneName;

    [SerializeField]
    private string m_gameOverMenuSceneName;

    [SerializeField]
    private string m_transitionSceneName;

    public event System.Action SceneLoadCompleted = delegate { };

    private List<string> m_scenesToLoad;

    private int m_numLoaded = 0;
   
    public void Initialize()
    {
        m_scenesToLoad = new List<string>() { m_mainMenuSceneName, m_gameplaySceneName, m_pauseMenuSceneName, m_gameOverMenuSceneName, m_transitionSceneName };
        m_numLoaded = 0;

        AsyncOperation mainMenuSceneLoadOperation = SceneManager.LoadSceneAsync(m_mainMenuSceneName, LoadSceneMode.Additive);
        mainMenuSceneLoadOperation.allowSceneActivation = true;
        mainMenuSceneLoadOperation.completed += OnSceneLoaded;

        AsyncOperation gameplaySceneLoadOperation = SceneManager.LoadSceneAsync(m_gameplaySceneName, LoadSceneMode.Additive);
        gameplaySceneLoadOperation.allowSceneActivation = false;
        gameplaySceneLoadOperation.completed += OnSceneLoaded;

        AsyncOperation transitionSceneLoadOperation = SceneManager.LoadSceneAsync(m_transitionSceneName, LoadSceneMode.Additive);
        transitionSceneLoadOperation.allowSceneActivation = false;
        transitionSceneLoadOperation.completed += OnSceneLoaded;

        AsyncOperation pauseMenuLoadOperation = SceneManager.LoadSceneAsync(m_pauseMenuSceneName, LoadSceneMode.Additive);
        pauseMenuLoadOperation.allowSceneActivation = false;
        pauseMenuLoadOperation.completed += OnSceneLoaded;

        AsyncOperation gameOverSceneLoadOperation = SceneManager.LoadSceneAsync(m_gameOverMenuSceneName, LoadSceneMode.Additive);
        gameOverSceneLoadOperation.allowSceneActivation = false;
        gameOverSceneLoadOperation.completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperation sceneLoadOperation)
    {
        sceneLoadOperation.completed -= OnSceneLoaded;
        m_numLoaded++;
        CheckIfAllScenesLoaded();
    }

    private void CheckIfAllScenesLoaded()
    {
        int numScenesToLoad = m_scenesToLoad.Count;
        if ( numScenesToLoad > 0 && m_numLoaded == numScenesToLoad )
        {
            SceneLoadCompleted.Invoke();
        }
    }
}
