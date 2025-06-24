using UnityEngine;

public class Movement : MonoBehaviour
{
    //GroundCheck
    public Transform GroundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public bool isGrounded;
    //Gravity Access
    private Rigidbody2D rb;
    //Movement Variables
    private float speed = 12.0f;
    private float jumpForce = 14.0f;
    //WallCheck
    public Transform WallCheck;
    public float wallCheckDistance = 0.5f;
    public LayerMask wallLayer;
    public bool isWallSliding;
    public float wallSlideSpeed = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _GroundCheck();
        _Move();
        _Jumping();
    }

    void _WallSlide()
    {
        bool isTouchingWall = Physics2D.Raycast(WallCheck.position, Vector2.right * transform.localScale.x, wallCheckDistance, wallLayer);
        bool isAirborne = !isGrounded; //See if it's in the Air or not

        if (isTouchingWall && isAirborne)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }
    }
    void _Jumping()
    {
        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    void _Move()
    {
        // Handle horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }
    void _GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, groundLayer);
    }
}
