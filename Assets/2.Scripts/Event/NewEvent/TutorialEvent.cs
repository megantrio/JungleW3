using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : EventObject
{
    public List<GameObject> tutorial;

    public override void StartEvent()
    {
        StartCoroutine(Tutorial());
    }

    public IEnumerator Tutorial()
    {
        Debug.Log("튜토리얼 시작");
        foreach(var t in tutorial)
        {
            t.SetActive(true);
            while (t.activeSelf)
            {
                yield return null;
            }
        }
       PostEventEnded();
    }
}
