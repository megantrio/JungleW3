using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOpener : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    #endregion

    public GameObject mainEvent;
    #region PrivateMethod
    private void Start()
    {
        //��ũ��Ʈ ���������� ���
        mainEvent.SetActive(true);
    }
    #endregion
}
