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
    private Player m_player;

    private List<GameObject> m_spawnedPrefabs = new();

    public void SetUpLevel()
    {
        m_player.DisableMovement();
        m_player.transform.position = m_playerStartPosition.position;

        Level currentLevelData = m_levelData[m_currentLevel - 1];
        List<GameObject> foodPrefabs = currentLevelData.GetFoodPrefabs;
        int numPrefabs = foodPrefabs.Count;
        int numSpawned = 0;
        int spawnIndex = 0;
        List<Transform> foodPositionOptions = new List<Transform>(m_foodSpawnPositions);
        while (numSpawned < numPrefabs)
        {
            Transform nextSpawnPos;
            if (numPrefabs - numSpawned < foodPositionOptions.Count)
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
            m_spawnedPrefabs.Add(prefab);
            numSpawned++;
            spawnIndex++;
        }

        m_player.EnableMovement();
    }

    public void CleanUpLevel()
    {
        for (int iPrefab = 0; iPrefab < m_spawnedPrefabs.Count; ++iPrefab)
        {
            Destroy(m_spawnedPrefabs[iPrefab]);
        }
        m_spawnedPrefabs.Clear();
    }

}
