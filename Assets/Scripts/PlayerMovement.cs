using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("movement")]
    public float acceleration = 10f;
    public float friction = 5.5f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float slideSpeed = 8f;
    public float moveSlide = 2f;
    public float slideSlowdown = .5f;
    [Header("wallSlide")]
    public float wallCheckRadius = 0.2f;
    public Transform wallCheck;
    public LayerMask wallLayer;

    private bool isTouchingWall;
    private bool isWallSliding;
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool jumpTriggered;
    private bool isSliding;
    private InputSystem_Actions inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (gameObject.tag == "Player1")
        {
            inputActions.Player1.Enable();
            inputActions.Player1.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
            inputActions.Player1.Move.canceled += ctx => movement = Vector2.zero;

            inputActions.Player1.Jump.performed += ctx => jumpTriggered = true;
        }
        if (gameObject.tag == "Player2")
        {
            inputActions.Player2.Enable();
            inputActions.Player2.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
            inputActions.Player2.Move.canceled += ctx => movement = Vector2.zero;

            inputActions.Player2.Jump.performed += ctx => jumpTriggered = true;
        }
    }
    private void OnDisable()
    {
        if (gameObject.tag == "Player1")
        {
            inputActions.Player1.Move.performed -= ctx => movement = ctx.ReadValue<Vector2>();
            inputActions.Player1.Move.canceled -= ctx => movement = Vector2.zero;
            inputActions.Player1.Jump.performed -= ctx => jumpTriggered = true;

            inputActions.Player1.Disable();
        }
        if (gameObject.tag == "Player2")
        {
            inputActions.Player2.Move.performed -= ctx => movement = ctx.ReadValue<Vector2>();
            inputActions.Player2.Move.canceled -= ctx => movement = Vector2.zero;
            inputActions.Player2.Jump.performed -= ctx => jumpTriggered = true;

            inputActions.Player2.Disable();
        }
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(wallCheck.position, wallCheckRadius, Vector2.zero, 0f, wallLayer);
        isTouchingWall = hit && Mathf.Abs(hit.normal.x) > 0.9f;
        isWallSliding = isTouchingWall && rb.linearVelocity.y < 0f;

        if (jumpTriggered)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpTriggered = false;
        }
        //Debug.Log("Move input: " + movement);
        // if (movement == Vector2.zero)
        // {
        //     Debug.Log("Start Sliding");
        //     if (isSliding == false)
        //     {
        //         isSliding = true;
        //         slideSpeed = rb.linearVelocity.x;
        //     }
        //     slideSpeed = slideSpeed-slideSlowdown;
        //     rb.linearVelocityX = slideSpeed;
        // }
        // else
        // {
        //     isSliding = false;
        // }
    }

    private void FixedUpdate()
    {
        float TargetSpeed = movement.x * moveSpeed;
        float SpeedDif = TargetSpeed - rb.linearVelocity.x;
        float Move = SpeedDif * acceleration * Time.fixedDeltaTime;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x + Move, rb.linearVelocity.y);

        if (movement.x == 0f && !isWallSliding)
        {
            float frictionForce = Mathf.Min(Mathf.Abs(rb.linearVelocity.x), friction);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x - Mathf.Sign(rb.linearVelocity.x) * frictionForce, rb.linearVelocity.y);
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -slideSpeed);
        }
    }
}
