using UnityEngine;

[CreateAssetMenu(fileName = "PlayerJitterEffect", menuName = "ScriptableObjects/Effects/PlayerJitterEffect")]
public class PlayerJitterEffect : Effect
{
    [SerializeField]
    private float m_playerJitterMin;

    [SerializeField]
    private float m_playerJitterMax;

    public override void Apply()
    {
        GameObject.FindFirstObjectByType<Player>().EnableJitter(m_playerJitterMin, m_playerJitterMax);
    }

    public override void Remove()
    {
        GameObject.FindFirstObjectByType<Player>().DisableJitter();
    }
}
