using UnityEngine;

[CreateAssetMenu(fileName = "BouncingMinimapEffect", menuName = "ScriptableObjects/Effects/BouncingMinimapEffect")]
public class BouncingMinimapEffect : Effect
{
    public override void Apply()
    {
        GameObject.FindFirstObjectByType<MinimapController>().StartBounce();
    }

    public override void Remove()
    {
        GameObject.FindFirstObjectByType<MinimapController>().StopBounce();
    }
}
