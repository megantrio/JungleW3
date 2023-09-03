using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventObject : MonoBehaviour
{
    //분기 관련 데이터
    public string condition = "";
    public void PostEventEnded()
    {
        if (DayManager.instance != null)
        {
            DayManager.instance.SetEventEnded(this);
        }
    }

    public abstract void StartEvent();
}
