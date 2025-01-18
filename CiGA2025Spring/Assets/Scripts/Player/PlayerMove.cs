using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float rotationSpeed = 10f;

    private float dashTime = 0f;
    private bool isDashing = false;
    private Vector2 dashDirection;

    private Rigidbody2D rb;
    private Transform spriteTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteTransform = transform.Find("Sprite");
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

        if (moveDirection == Vector2.zero)
        {
            float targetAngle = 0f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            spriteTransform.rotation = Quaternion.Slerp(spriteTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            spriteTransform.rotation = Quaternion.Slerp(spriteTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
        Messenger.Broadcast<bool>(MsgType.PlayerDash, isDashing);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        dashDirection = new Vector2(horizontal, vertical).normalized;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = Vector2.up;
        }

        if (dashDirection != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(dashDirection.y, dashDirection.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            spriteTransform.rotation = Quaternion.Slerp(spriteTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Invoke("EndDash", dashDuration);
    }

    void EndDash()
    {
        isDashing = false;
        dashTime = dashCooldown;
        Messenger.Broadcast<bool>(MsgType.PlayerDash, isDashing);
    }

    public Vector2 GetMoveDirection()
    {
        return rb.velocity.normalized;
    }
}
