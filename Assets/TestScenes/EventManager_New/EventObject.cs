using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : MonoBehaviour
{
    //�̺�Ʈ�Ŵ������� ��� �̺�Ʈ�� �ε��� �� ȣ��˴ϴ�.
    public EventObject nextEvent = null;
    //�б� ���� ������
    public string condition = "";
    public void PostEventEnded()
    {
        if (DayManager.instance != null)
        {
            DayManager.instance.SetEventEnded(this);
        }
    }
}
