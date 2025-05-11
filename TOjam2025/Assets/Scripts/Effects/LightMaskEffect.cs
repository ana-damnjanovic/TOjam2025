using UnityEngine;

[CreateAssetMenu(fileName = "LightMaskEffect", menuName = "ScriptableObjects/Effects/LightMaskEffect")]
public class LightMaskEffect : Effect
{
    public override void Apply()
    {
        GameObject.FindFirstObjectByType<Player>().EnableLightMask();
    }

    public override void Remove()
    {
        GameObject.FindFirstObjectByType<Player>().DisableLightMask();
    }
}
