using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    #region PublicVariables
    public InteractEvent triggered;
   
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    public void OnObjectTrigger()
    {
        if (triggered != null)
        {
            triggered.connected.SetActive(true);
        }
    }
    #endregion

    #region PrivateMethod
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactor"))
        {
            triggered = collision.gameObject.GetComponent<InteractEvent>();
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
