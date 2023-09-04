using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventObject : MonoBehaviour
{
    //분기 관련 데이터
    [Header("DataManager의 해당 Condition이 True일 경우에만 실행됩니다.")]
    public string condition = "";
    [HideInInspector] public bool conditionValue = true;
    public void PostEventEnded()
    {
        if (DayManager.instance != null)
        {
            DayManager.instance.SetEventEnded(this);
        }
    }

    public abstract void StartEvent();
}
