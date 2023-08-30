using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractEvent : MonoBehaviour
{
    #region PublicVariables
    public GameObject connected;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
    #endregion
}
