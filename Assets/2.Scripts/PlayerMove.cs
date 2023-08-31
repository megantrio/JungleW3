using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerMove : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb;

    public float moveSpeed = 1f;

    private float cameraSpeed = 3f;
    private Camera mainCamera;
    private Vector3 playerPosition;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.x = transform.position.x;
        cameraPosition.y = transform.position.y;
        mainCamera.transform.position = cameraPosition;

        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        animator.SetBool("isRun", true);
        moveInput = context.ReadValue<Vector2>();
    }
   

}
