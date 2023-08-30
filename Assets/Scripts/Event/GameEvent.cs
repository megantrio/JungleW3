using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    //�̺�Ʈ�Ŵ������� ������
    #region PublicVariables
    public string eventName;
    public GameEvent nextEvent;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    public void Init()
    {
        Debug.Log("����" + eventName);
    }

    #endregion

    #region PrivateMethod
    private void Start()
    {
        //�̺�Ʈ�� ���� �� ���������
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Init();
    }
    #endregion
}
