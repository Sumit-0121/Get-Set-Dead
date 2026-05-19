using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 10f;

    [Header("Double Jump")]
    public int maxJumps = 1;

    [Header("Better Jump")]
    public float fallMultiplier = 18.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    private bool isGrounded;
    private int jumpsLeft;

    // Squash & Stretch
    private Vector3 originalScale;

    // Flip
    private bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        originalScale = transform.localScale;

        jumpsLeft = maxJumps;
    }

    void Update()
    {
        HandleMovement();

        HandleGroundCheck();

        HandleJump();

        BetterJumpPhysics();
    }

    // =========================================
    // MOVEMENT
    // =========================================
    void HandleMovement()
    {
        float moveInput = 0f;

        // Keyboard Input
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
        }

        // Mobile Input
        if (MobileInput.moveLeft)
        {
            moveInput = -1f;
        }

        if (MobileInput.moveRight)
        {
            moveInput = 1f;
        }

        rb.linearVelocity =
            new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip Character
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    // =========================================
    // GROUND CHECK
    // =========================================
    void HandleGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (isGrounded)
        {
            jumpsLeft = maxJumps;
        }
    }

    // =========================================
    // JUMP
    // =========================================
    void HandleJump()
    {
        bool jumpInput =
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            MobileInput.jumpPressed;

        if (jumpInput && jumpsLeft > 0)
        {
            rb.linearVelocity =
                new Vector2(rb.linearVelocity.x, jumpForce);

            jumpsLeft--;

            AudioManager.instance?.PlayJump();

            SquashAndStretch();
        }
    }

    // =========================================
    // BETTER JUMP
    // =========================================
    void BetterJumpPhysics()
    {
        // Faster Fall
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity +=
                Vector2.up * Physics2D.gravity.y *
                (fallMultiplier - 1) * Time.deltaTime;
        }

        // Short Hop
        else if (
            rb.linearVelocity.y > 0 &&
            !Input.GetKey(KeyCode.Space) &&
            !MobileInput.jumpPressed
        )
        {
            rb.linearVelocity +=
                Vector2.up * Physics2D.gravity.y *
                (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    // =========================================
    // FLIP
    // =========================================
    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;

        scale.x *= -1;

        transform.localScale = scale;
    }

    // =========================================
    // SQUASH & STRETCH
    // =========================================
    void SquashAndStretch()
    {
        float direction = Mathf.Sign(transform.localScale.x);

        transform.localScale =
            new Vector3(
                Mathf.Abs(originalScale.x) * 1.15f * direction,
                originalScale.y * 0.85f,
                1f
            );

        Invoke(nameof(ResetScale), 0.08f);
    }

    void ResetScale()
    {
        float direction = Mathf.Sign(transform.localScale.x);

        transform.localScale =
            new Vector3(
                Mathf.Abs(originalScale.x) * direction,
                originalScale.y,
                originalScale.z
            );
    }

    // =========================================
    // GIZMOS
    // =========================================
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );
    }
}