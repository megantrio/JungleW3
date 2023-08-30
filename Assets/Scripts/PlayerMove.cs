using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    public void OnMovement(InputAction.CallbackContext callback)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        Vector2 move = (callback.ReadValue<Vector2>());
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = move;
        if (callback.canceled)
        {
            rb.velocity = new Vector2();
        }
    }
    #endregion

    #region PrivateMethod
    #endregion
}
