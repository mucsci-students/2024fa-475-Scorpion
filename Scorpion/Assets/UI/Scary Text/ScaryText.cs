using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaryText : MonoBehaviour
{
    [SerializeField] private AudioSource src;
    [SerializeField] private AudioClip muahahaha1;
    [SerializeField] private AudioClip muahahaha2;
    [SerializeField] private AudioClip gong;
    [SerializeField] private List<Sprite> enteringText;
    [SerializeField] private List<float> enteringTextDuration;
    [SerializeField] private Sprite childEnteringSprite;
    [SerializeField] private List<Sprite> treasureRoomText;
    [SerializeField] private List<float> treasureRoomTextDuration;
    [SerializeField] private Sprite childTreasureRoomSprite;
    [SerializeField] private float displayTime = 0.5f;
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private BGMController bgmController;

    private Coroutine sequenceCoroutine;
    private Image im;
    private RectTransform rectTransform;
    private ScaryTextChild child;

    void Start ()
    {
        im = GetComponent<Image> ();
        im.color = new Color (1f, 1f, 1f, 0f);
        im.preserveAspect = true;
        rectTransform = GetComponent<RectTransform> ();
        child = GetComponentInChildren<ScaryTextChild> ();
    }
    
    public void PlayEnteringSequence ()
    {
        sequenceCoroutine = StartCoroutine (EnteringSequence ());
    }

    public void PlayTreasureRoomSequence ()
    {
        sequenceCoroutine = StartCoroutine (TreasureRoomSequence ());
    }

    private IEnumerator EnteringSequence ()
    {
        float duration = 0f;
        float startTime = float.NegativeInfinity;
        int i = 0;
        while (true)
        {
            if (startTime + duration < Time.time)
            {
                if (i == enteringTextDuration.Count)
                    break;
                src.PlayOneShot (gong);
                if (i == 2)
                    src.PlayOneShot (muahahaha2);
                duration = enteringTextDuration[i];
                startTime = Time.time;
                Display (enteringText[i]);
                child.Display (childEnteringSprite);
                ++i;
            }
            else
            {
                Fade (startTime, displayTime, fadeTime, duration);
                if (i == 1)
                    child.Fade (startTime, displayTime, fadeTime, duration);
            }

            yield return null;
        }
    }

    private IEnumerator TreasureRoomSequence ()
    {
        float duration = 0f;
        float startTime = float.NegativeInfinity;
        int i = 0;
        while (true)
        {
            if (startTime + duration < Time.time)
            {
                if (i == treasureRoomTextDuration.Count)
                    break;
                if (i == 1)
                    bgmController.BeginFinalFight ();
                src.PlayOneShot (gong);
                if (i == 3)
                    src.PlayOneShot (muahahaha1);
                duration = treasureRoomTextDuration[i];
                startTime = Time.time;
                Display (treasureRoomText[i]);
                child.Display (childTreasureRoomSprite);
                ++i;
            }
            else
            {
                Fade (startTime, displayTime, fadeTime, duration);
                if (i == 1)
                    child.Fade (startTime, displayTime, fadeTime, duration);
            }

            yield return null;
        }
    }

    private void Display (Sprite sprite)
    {
        im.sprite = sprite;
    }

    private void Fade (float startTime, float displayTime, float fadeTime, float duration)
    {
        if (startTime + displayTime > Time.time)
            im.color += new Color (1f, 1f, 1f, Time.deltaTime / displayTime);
        else if (startTime + duration - fadeTime < Time.time)
            im.color += new Color (1f, 1f, 1f, -Time.deltaTime / fadeTime);
    }

}
