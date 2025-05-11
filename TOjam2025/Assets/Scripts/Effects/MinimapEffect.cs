using UnityEngine;

[CreateAssetMenu(fileName = "MinimapEffect", menuName = "ScriptableObjects/Effects/MinimapEffect")]
public class MinimapEffect : Effect
{
    public override void Apply()
    {
        GameObject.FindFirstObjectByType<MinimapController>().EnableMinimap();
    }

    public override void Remove()
    {
        GameObject.FindFirstObjectByType<MinimapController>().DisableMinimap();
    }
}
