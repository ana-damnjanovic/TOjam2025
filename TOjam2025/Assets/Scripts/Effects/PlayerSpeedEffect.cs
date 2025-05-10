using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSpeedEffect", menuName = "ScriptableObjects/Effects/PlayerSpeedEffect")]
public class PlayerSpeedEffect : Effect
{
    [SerializeField]
    private float m_playerSpeedModifier;

    public override void Apply()
    {
        GameObject.FindFirstObjectByType<Player>().ApplySpeedModifier(m_playerSpeedModifier);
    }

    public override void Remove()
    {
        GameObject.FindFirstObjectByType<Player>().ApplySpeedModifier(1f);
    }
}
