using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class GameEvent : MonoBehaviour
{
    public abstract void StartEvent();
    public void EndEvent()
    {
        MorningEventManager.instance.isEventEnded = true;
        gameObject.SetActive(false);
    }
}
