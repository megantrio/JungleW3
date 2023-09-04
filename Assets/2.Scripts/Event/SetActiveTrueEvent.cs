using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveTrueEvent : EventObject
{
    public GameObject target;
    public bool val = true;
    public override void StartEvent()
    {
        target.SetActive(val);
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        PostEventEnded();
    }
}
