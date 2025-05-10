using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    [SerializeField]
    private int m_levelNum;

    [SerializeField]
    private Effect m_levelEffect;

    [SerializeField]
    private List<GameObject> m_foodPrefabs;

    public int GetLevelNum => m_levelNum;
    public List<GameObject> GetFoodPrefabs => m_foodPrefabs;

    public Effect GetLevelEffect => m_levelEffect;
}
