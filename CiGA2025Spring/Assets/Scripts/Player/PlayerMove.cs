using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float rotationSpeed = 10f;
    public int playerNum;

    private float dashTime = 0f;
    private bool isDashing = false;
    private bool canMove = true;
    private Vector2 dashDirection;

    private Rigidbody2D rb;
    private Transform spriteTransform;

    private Vector3 playerOriginalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteTransform = transform.Find("Sprite");

        Messenger.AddListener(MsgType.ResetPlayer, ResetPlayer);

        if (playerNum == 1)
        {
            playerOriginalPosition = new Vector3(-2.5f, 0, 0);
            Messenger.AddListener(MsgType.Player1IsDying, PlayerGameOver);
        }
        else if (playerNum == 2)
        {
            playerOriginalPosition = new Vector3(2.5f, 0, 0);
            Messenger.AddListener(MsgType.Player2IsDying, PlayerGameOver);
        }
    }

    private void PlayerGameOver()
    {
        canMove = false;
        rb.velocity = Vector2.zero;
    }

    private void ResetPlayer()
    {
        isDashing = false;
        canMove = true;
        dashTime = 0f;
        rb.velocity = Vector2.zero;
        transform.position = playerOriginalPosition;
    }

    void Update()
    {
        if (canMove)
        {
            HandleMovement();
            HandleDash();
        }
    }

    void HandleMovement()
    {
        string horizontalInput = playerNum == 1 ? "Horizontal" : "Horizontal2";
        string verticalInput = playerNum == 1 ? "Vertical" : "Vertical2";

        float horizontal = Input.GetAxisRaw(horizontalInput);
        float vertical = Input.GetAxisRaw(verticalInput);

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
        if (playerNum == 1 && Input.GetKeyDown(KeyCode.Space) && dashTime <= 0f)
        {
            Messenger.Broadcast(MsgType.ChangeBubbleBar, 1, -GlobalData.DashBubbleConsumption);
            StartDash();
        }
        else if (playerNum == 2 && Input.GetKeyDown(KeyCode.Return) && dashTime <= 0f)
        {
            Messenger.Broadcast(MsgType.ChangeBubbleBar, 2, -GlobalData.DashBubbleConsumption);
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

        string horizontalInput = playerNum == 1 ? "Horizontal" : "Horizontal2";
        string verticalInput = playerNum == 1 ? "Vertical" : "Vertical2";

        float horizontal = Input.GetAxisRaw(horizontalInput);
        float vertical = Input.GetAxisRaw(verticalInput);
        dashDirection = new Vector2(horizontal, vertical).normalized;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = Vector2.up;

            GameObject bubblePrefab = Resources.Load<GameObject>("Prefabs/Map/LittleBubble");
            GameObject bubbleInstance = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            bubbleInstance.AddComponent<PlayerBubbleTrail>();
            bubbleInstance.GetComponent<PlayerBubbleTrail>().SetDirection(Vector2.down);
        }

        if (dashDirection != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(dashDirection.y, dashDirection.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            spriteTransform.rotation = Quaternion.Slerp(spriteTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            GameObject bubblePrefab = Resources.Load<GameObject>("Prefabs/Map/LittleBubble");
            GameObject bubbleInstance = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            Vector2 bubbleDirection = -dashDirection;
            bubbleInstance.AddComponent<PlayerBubbleTrail>();
            bubbleInstance.GetComponent<PlayerBubbleTrail>().SetDirection(bubbleDirection);
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
