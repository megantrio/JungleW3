using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkUIOpen : MonoBehaviour
{
    #region PublicVariables
    public GameObject triggered;
   
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    public void OnObjectTrigger()
    {
        if (triggered != null)
        {
            triggered.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    #endregion

    #region PrivateMethod
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactor"))
        {
            triggered = collision.gameObject;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactor"))
        {
            triggered = null;
        }
    }
    #endregion
}
