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
    private SpriteRenderer spriteRenderer;
    private PlayerItemGet pig;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
        spriteRenderer = GetComponent<SpriteRenderer>();
        pig = GetComponent<PlayerItemGet>();
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
        //현재 사용하지 않음
        return;
        if (MorningEventManager.instance.state != MorningEventManager.GameState.NIGHT)
        {
            return;
        }
        if (context.started)
        {
            animator.SetBool("isRun", true);
            
        }
        moveInput = context.ReadValue<Vector2>();
        if (pig)
        {
            Debug.Log("pig");
            if (pig.newsUI)
            {
                Debug.Log("news");
                pig.newsUI.SetActive(false);
            }
            if(pig.listUI)
                pig.listUI.SetActive(false);
        }
        if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if(moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (context.canceled)
        {
            animator.SetBool("isRun", false);
        }
    }

}
