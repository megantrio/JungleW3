using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventObject : MonoBehaviour
{
    //�б� ���� ������
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
