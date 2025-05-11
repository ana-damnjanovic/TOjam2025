using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int m_currentLevel = 1;

    [SerializeField]
    private List<Level> m_levelData;

    [SerializeField]
    private Transform m_playerStartPosition;

    [SerializeField]
    private List<Transform> m_foodSpawnPositions;

    [SerializeField]
    private List<Transform> m_enemySpawnPositions;

    [SerializeField]
    private Player m_player;

    [SerializeField]
    private PlayerGoal m_playerGoal;

    [SerializeField]
    private List<GameObject> m_enemyPrefabs;

    private List<GameObject> m_spawnedFoodPrefabs = new();

    private List<GameObject> m_spawnedEnemyPrefabs = new ();

    private List<Effect> m_activeLevelEffects = new();

    public event System.Action LevelSucceeded = delegate { };

    public event System.Action AllLevelsWon = delegate { };

    public int GetCurrentLevel => m_currentLevel;
    public int GetMaxLevel => m_levelData.Count;

    public string GetLevelText => m_levelData[m_currentLevel-1].GetLevelText;

    public void Initialize()
    {
        SetUpLevel();
        AddListeners();
    }

    public void SetUpLevel()
    {
        m_player.DisableMovement();
        m_player.transform.position = m_playerStartPosition.position;

        // spawn food
        Level currentLevelData = m_levelData[m_currentLevel - 1];
        List<GameObject> foodPrefabs = currentLevelData.GetFoodPrefabs;
        int numFoodPrefabs = foodPrefabs.Count;
        int numFoodItemsSpawned = 0;
        int spawnIndex = 0;
        List<Transform> foodPositionOptions = new List<Transform>(m_foodSpawnPositions);
        while (numFoodItemsSpawned < numFoodPrefabs)
        {
            Transform nextSpawnPos;
            if (numFoodPrefabs - numFoodItemsSpawned < foodPositionOptions.Count)
            {
                int randomPosIndex = Random.Range(0, foodPositionOptions.Count - 1);
                nextSpawnPos = foodPositionOptions[randomPosIndex];
                foodPositionOptions.RemoveAt(randomPosIndex);
            }
            else
            {
                nextSpawnPos = foodPositionOptions[0];
                foodPositionOptions.RemoveAt(0);
            }
            GameObject prefab = GameObject.Instantiate(foodPrefabs[spawnIndex], nextSpawnPos);
            m_spawnedFoodPrefabs.Add(prefab);
            numFoodItemsSpawned++;
            spawnIndex++;
        }

        // spawn enemies
        int numEnemiesRequired = currentLevelData.GetNumEnemies;
        int numEnemiesSpawned = 0;
        List<Transform> enemyPositionOptions = new List<Transform>(m_enemySpawnPositions);
        while (numEnemiesSpawned < numEnemiesRequired && enemyPositionOptions.Count > 0)
        {
            int randomPosIndex = Random.Range(0, enemyPositionOptions.Count - 1);
            Transform spawnPos = enemyPositionOptions[randomPosIndex];
            enemyPositionOptions.RemoveAt(randomPosIndex);

            GameObject prefab = PickRandomEnemyPrefab();
            GameObject spawnedEnemy = GameObject.Instantiate(prefab, spawnPos);
            m_spawnedEnemyPrefabs.Add(spawnedEnemy);
            numEnemiesSpawned++;
            spawnIndex++;
        }


        // apply level effects
        Effect levelEffect = currentLevelData.GetLevelEffect;
        if (null != levelEffect)
        {
            levelEffect.Apply();
            m_activeLevelEffects.Add(levelEffect);
        }

        m_player.EnableMovement();
    }

    public void Reset()
    {
        CleanUpLevel();
        for (int iEffect = 0; iEffect < m_activeLevelEffects.Count; ++iEffect)
        {
            m_activeLevelEffects[iEffect].Remove();
        }
        m_activeLevelEffects.Clear();
        m_currentLevel = 1;
    }

    private GameObject PickRandomEnemyPrefab()
    {
        int prefabIndex = Random.Range(0, m_enemyPrefabs.Count);
        return m_enemyPrefabs[prefabIndex];
    }

    private void CleanUpLevel()
    {
        for (int iFoodPrefab = 0; iFoodPrefab < m_spawnedFoodPrefabs.Count; ++iFoodPrefab)
        {
            Destroy(m_spawnedFoodPrefabs[iFoodPrefab]);
        }
        for (int iEnemyPrefab = 0; iEnemyPrefab < m_spawnedEnemyPrefabs.Count; ++iEnemyPrefab)
        {
            Destroy(m_spawnedEnemyPrefabs[iEnemyPrefab]);
        }
        m_spawnedFoodPrefabs.Clear();
    }


    private void AddListeners()
    {
        m_player.LevelFailed += OnLevelFailed;
        m_playerGoal.PlayerReachedGoal += OnLevelSucceeded;
    }

    private void RemoveListeners()
    {
        m_player.LevelFailed -= OnLevelFailed;
        m_playerGoal.PlayerReachedGoal -= OnLevelSucceeded;
    }

    private void OnLevelSucceeded()
    {
        CleanUpLevel();
        m_currentLevel++;

        RemoveListeners();

        if (m_currentLevel > m_levelData.Count)
        {
            AllLevelsWon.Invoke();
        }
        else
        {
            LevelSucceeded.Invoke();
        }
    }

    private void OnLevelFailed()
    {
        RemoveListeners();
        CleanUpLevel();
        Initialize();
    }
}
