using System.Collections;
using UnityEngine;

public class SpriteJitter : MonoBehaviour
{
    [SerializeField]
    private Material m_spriteJitterMaterial;

    [SerializeField]
    private SpriteRenderer m_spriteRenderer;

    private Material m_spriteOriginalMaterial;

    public void StartJitter()
    {
        m_spriteOriginalMaterial = m_spriteRenderer.material;
        m_spriteRenderer.material = m_spriteJitterMaterial;
    }

    public void StopJitter()
    {
        m_spriteRenderer.material = m_spriteOriginalMaterial;
    }

}
