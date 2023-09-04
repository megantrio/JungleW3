using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOutEvent : EventObject
{
    public float fadeOutTime = 1.0f;
    public void FadeOut(float fadeOutTime, System.Action nextEvent = null)
    {
        gameObject.SetActive(true);
        StartCoroutine(CoFadeOut(fadeOutTime, nextEvent));
    }

    public override void StartEvent()
    {
        FadeOut(fadeOutTime);
    }

    // 투명 -> 불투명
    IEnumerator CoFadeOut(float fadeOutTime, System.Action nextEvent = null)
    {
        Image sr = this.gameObject.GetComponent<Image>();
        Color tempColor = sr.color;
        tempColor.a = 1.0f;
        while (tempColor.a > 0f)
        {
            tempColor.a -= Time.deltaTime / fadeOutTime;
            sr.color = tempColor;

            if (tempColor.a <= 0f) tempColor.a = 0f;

            yield return null;
        }

        sr.color = tempColor;
        if (nextEvent != null) nextEvent();

        gameObject.SetActive(false);
        PostEventEnded();
    }


}