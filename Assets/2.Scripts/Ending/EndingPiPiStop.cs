using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPiPiStop : EventObject
{
    public float waitTime = 2.0f;
    public override void StartEvent()
    {
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(waitTime);
        PostEventEnded();
    }
}
