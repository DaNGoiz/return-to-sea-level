using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private float dashTime = 0f;
    private bool isDashing = false;
    private Vector2 dashDirection;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
        HandleDash();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        if (isDashing)
        {
            rb.velocity = dashDirection * dashSpeed;
        }
        else
        {
            rb.velocity = moveDirection * moveSpeed;
        }
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashTime <= 0f)
        {
            StartDash();
        }

        if (dashTime > 0f)
        {
            dashTime -= Time.deltaTime;
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        dashDirection = new Vector2(horizontal, vertical).normalized;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = Vector2.right;
        }

        Invoke("EndDash", dashDuration);
    }

    void EndDash()
    {
        isDashing = false;
        dashTime = dashCooldown;
    }

    public Vector2 GetMoveDirection()
    {
        return rb.velocity.normalized;
    }
}
