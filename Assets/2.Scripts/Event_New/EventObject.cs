using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventObject : MonoBehaviour
{
    //�б� ���� ������
    [Header("DataManager�� �ش� Condition�� True�� ��쿡�� ����˴ϴ�.")]
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
