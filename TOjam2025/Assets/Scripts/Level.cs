using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    [SerializeField]
    private int m_levelNum;

    [SerializeField]
    private string m_levelText;

    [SerializeField]
    private Effect m_levelEffect;

    [SerializeField]
    private List<GameObject> m_foodPrefabs;

    public int GetLevelNum => m_levelNum;

    public string GetLevelText => m_levelText;

    public List<GameObject> GetFoodPrefabs => m_foodPrefabs;

    public Effect GetLevelEffect => m_levelEffect;
}
