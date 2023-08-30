using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : GameEvent
{
    public List<GameObject> tutorial;

    public override void StartEvent()
    {
        StartCoroutine(Tutorial());
    }

    public IEnumerator Tutorial()
    {
        Debug.Log("»ý°¢");
        foreach(var t in tutorial)
        {
            t.SetActive(true);
            while (t.activeSelf)
            {
                yield return null;
            }
        }

        EndEvent();
    }
}
