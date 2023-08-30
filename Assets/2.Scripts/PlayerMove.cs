using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    private void Update()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }
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
