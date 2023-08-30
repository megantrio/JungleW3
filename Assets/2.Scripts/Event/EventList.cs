using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventList : GameEvent
{
    #region PublicVariables
    public GameEvent[] list;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    private void Awake()
    {
        if (list.Length > 1)
        {
            nextEvent = list[1];
        }
        for (int i=0;i<list.Length-1; i++)
        {
            list[i].nextEvent = list[i + 1];
        }
    }
    private void Start()
    {
    }

    private void OnValidate()
    {
        if (list.Length > 1)
        {
            nextEvent = list[1];
        }
        list = GetComponentsInChildren<GameEvent>(); 
    }
    #endregion
}
