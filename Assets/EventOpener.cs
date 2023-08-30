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
        //스크립트 순서때문에 사용
        mainEvent.SetActive(true);
    }
    #endregion
}
