using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public static float infBubble = 1;
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
    private Collider2D playerBottleCollider; // 不是它自己的，而是子物体BottleHitCollider的碰撞体
    private AudioSource audioSource;
    private GameObject hollyLight;
    

    void Start()
    {
        hollyLight = transform.Find("HollyLight").gameObject;
        hollyLight.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        spriteTransform = transform.Find("Sprite");
        playerBottleCollider = FindChildByName(transform, "BottleHitCollider").GetComponent<Collider2D>();
        playerBottleCollider.enabled = false;

        Messenger.AddListener(MsgType.ResetPlayer, ResetPlayer);
        Messenger.AddListener<int, float>(MsgType.InfBubble, PlayerSuperPower);

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

    private Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }
            Transform result = FindChildByName(child, name);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

    public void PlayerSuperPower(int playerNum, float duration)
    {
        if (this.playerNum != playerNum)
        {
            return;
        }
        StartCoroutine(SuperPowerCoroutine(duration));
    }

    IEnumerator SuperPowerCoroutine(float duration)
    {
        hollyLight.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
        GetComponent<IPlayerDamagable>().CanHurt = false;
        yield return new WaitForSeconds(duration);
        hollyLight.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        GetComponent<IPlayerDamagable>().CanHurt = true;
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
            StartDash();
        }
        else if (playerNum == 2 && Input.GetKeyDown(KeyCode.Return) && dashTime <= 0f)
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
        playerBottleCollider.enabled = true;
        audioSource.Play();

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

            float bubbleScale = Random.Range(0.5f, 1.2f);
            bubbleInstance.AddComponent<PlayerBubbleTrail>();
            bubbleInstance.transform.localScale = new Vector3(bubbleScale, bubbleScale, 1);
            bubbleInstance.GetComponent<PlayerBubbleTrail>().SetDirection(Vector2.down);
            Messenger.Broadcast(MsgType.ChangeBubbleBar, playerNum, -GlobalData.DashBubbleConsumption * bubbleScale * infBubble);
        }

        if (dashDirection != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(dashDirection.y, dashDirection.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            spriteTransform.rotation = Quaternion.Slerp(spriteTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            GameObject bubblePrefab = Resources.Load<GameObject>("Prefabs/Map/LittleBubble");
            GameObject bubbleInstance = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            Vector2 bubbleDirection = -dashDirection;
            
            float bubbleScale = Random.Range(0.5f, 1.2f);
            bubbleInstance.AddComponent<PlayerBubbleTrail>();
            bubbleInstance.transform.localScale = new Vector3(bubbleScale, bubbleScale, 1);
            bubbleInstance.GetComponent<PlayerBubbleTrail>().SetDirection(bubbleDirection);
            Messenger.Broadcast(MsgType.ChangeBubbleBar, playerNum, -GlobalData.DashBubbleConsumption * bubbleScale * infBubble);
        }

        Invoke("EndDash", dashDuration);
    }

    void EndDash()
    {
        isDashing = false;
        dashTime = dashCooldown;
        playerBottleCollider.enabled = false;
    }

    public Vector2 GetMoveDirection()
    {
        return rb.velocity.normalized;
    }
}
