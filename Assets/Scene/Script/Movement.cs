using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 12f;
    public float jumpForce = 14f;
    public float wallSlideSpeed = 1f;

    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    private Rigidbody2D rb;
    public bool isGrounded;
    private bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpDirection;

    public Vector2 wallJumpForce = new Vector2(8f, 15f);
    public float wallJumpDuration = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _GroundCheck();
        _WallSlide();    // check before move
        _WallJumping();  // check before move
        _Jump();
        _Move();
        _Duck();
    }

    void _Duck()
    {
        if (Input.GetKeyDown("s") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * moveSpeed * 0.75f, rb.linearVelocity.y);
        }
    }

    void _Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }
    }

    void _GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    void _Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void _WallSlide()
    {
        bool isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.1f, wallLayer);

        isWallSliding = isTouchingWall && !isGrounded && rb.linearVelocity.y < 0;

        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }
    }

    void _WallJumping()
    {
        if (isWallSliding && Input.GetButtonDown("Jump"))
        {
            isWallJumping = true;
            wallJumpDirection = -Mathf.Sign(transform.localScale.x); // away from wall

            rb.linearVelocity = new Vector2(wallJumpForce.x * wallJumpDirection, wallJumpForce.y);

            // Flip player
            if ((wallJumpDirection > 0 && transform.localScale.x < 0) ||
                (wallJumpDirection < 0 && transform.localScale.x > 0))
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            Invoke(nameof(_StopWallJumping), wallJumpDuration);
        }
    }

    void _StopWallJumping()
    {
        isWallJumping = false;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
        if (wallCheck != null)
            Gizmos.DrawWireSphere(wallCheck.position, 0.1f);
    }
}
