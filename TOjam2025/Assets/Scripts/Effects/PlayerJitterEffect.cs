using UnityEngine;

[CreateAssetMenu(fileName = "PlayerJitterEffect", menuName = "ScriptableObjects/Effects/PlayerJitterEffect")]
public class PlayerJitterEffect : Effect
{
    [SerializeField]
    private float m_jitterMin;

    [SerializeField]
    private float m_jitterMax;

    public override void Apply()
    {
        GameObject.FindFirstObjectByType<Player>().EnableJitter(m_jitterMin, m_jitterMax);
    }

    public override void Remove()
    {
        GameObject.FindFirstObjectByType<Player>().DisableJitter();
    }
}
