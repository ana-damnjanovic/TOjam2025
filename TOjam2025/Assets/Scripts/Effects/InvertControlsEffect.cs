using UnityEngine;

public class InvertControlsEffect : Effect
{
    public override void Apply()
    {
        GameObject.FindFirstObjectByType<Player>().InvertControls(true);
    }

    public override void Remove()
    {
        GameObject.FindFirstObjectByType<Player>().InvertControls(false);
    }
}
