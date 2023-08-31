using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    private Vector3 oldPos;
    private void OnEnable()
    {
        oldPos = Camera.main.transform.position;
        Camera.main.transform.position = transform.position;
    }

    private void OnDisable()
    {
        Camera.main.transform.position = oldPos;
    }
    #endregion
}
