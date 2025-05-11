using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutscenePlay : MonoBehaviour
{
    [System.Serializable]
    public class CutsceneFrame
    {
        public Sprite image;
        [TextArea]
        public string text;
    }

    public Image cutsceneImage;
    public TMP_Text cutsceneText;
    public List<CutsceneFrame> frames = new List<CutsceneFrame>();

    public float frameDuration = 4f;

    private float timer = 0f;

    private int currentFrame = 0;

    private bool m_cutsceneStarted = false;

    public event System.Action CutsceneFinished = delegate { };

    public void PlayCutscene()
    {
        m_cutsceneStarted = true;
        if (frames.Count > 0)
        {
            ShowFrame(currentFrame);
        }
        else
        {
            Debug.LogWarning("CutsceneManager has no frames assigned.");
        }
    }

    void Update()
    {
        if (m_cutsceneStarted && currentFrame < frames.Count)
        {
            timer += Time.deltaTime;
            if (timer >= frameDuration)
            {
                timer = 0f;
                NextFrame();
            }
        }
    }

    void ShowFrame(int index)
    {
        if (index < 0 || index >= frames.Count)
            return;

        cutsceneImage.sprite = frames[index].image;
        cutsceneText.text = frames[index].text;
    }

    void NextFrame()
    {
        currentFrame++;
        if (currentFrame >= frames.Count)
        {
            EndCutscene();
        }
        else
        {
            ShowFrame(currentFrame);
        }
    }

    void EndCutscene()
    {
        CutsceneFinished.Invoke();
    }
}