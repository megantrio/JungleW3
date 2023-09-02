using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : MonoBehaviour
{
    //이벤트매니저에서 모든 이벤트를 로드할 때 호출됩니다.
    public EventObject nextEvent = null;
    //분기 관련 데이터
    public string condition = "";
    public void PostEventEnded()
    {
        if (DayManager.instance != null)
        {
            DayManager.instance.SetEventEnded(this);
        }
    }
}
