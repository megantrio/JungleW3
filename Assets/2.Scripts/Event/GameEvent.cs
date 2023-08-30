using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    //이벤트매니저에서 관리됨
    #region PublicVariables
    public string eventName;
    public GameEvent nextEvent;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    public void Init()
    {
        Debug.Log("시작" + eventName);
    }

    #endregion

    #region PrivateMethod
    private void Start()
    {
        //이벤트는 시작 시 사라져야함
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Init();
    }
    #endregion
}
