using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInEvent : EventObject
{
    public float fadeOutTime = 1.0f;
    public void FadeIn(float fadeOutTime, System.Action nextEvent = null)
    {
        gameObject.SetActive(true);
        StartCoroutine(CoFadeIn(fadeOutTime, nextEvent));
    }

    public override void StartEvent()
    {
        FadeIn(fadeOutTime);
    }

    // ���� -> ������
    IEnumerator CoFadeIn(float fadeOutTime, System.Action nextEvent = null)
    {
        Image sr = this.gameObject.GetComponent<Image>();
        Color tempColor = sr.color;
        tempColor.a = 0.0f;
        while (tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / fadeOutTime;
            sr.color = tempColor;

            if (tempColor.a >= 1f) tempColor.a = 1f;

            yield return null;
        }

        sr.color = tempColor;
        if (nextEvent != null) nextEvent();

        gameObject.SetActive(false);
        PostEventEnded();
    }
}