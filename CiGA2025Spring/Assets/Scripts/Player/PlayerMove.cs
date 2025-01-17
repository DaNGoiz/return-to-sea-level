using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool CanMove { get; set; }
    public Vector2 MovementDir { get; set; }
    private Vector2 velocity;
    private Vector2 resistance;
    public Vector2 Velocity
    {
        get
        {
            return velocity;
        }
        set
        {
            if (value.magnitude > PlayerMaxSpeed)
            {
                velocity = value.normalized * PlayerMaxSpeed;
            }
            else
            {
                velocity = value;
            }
        }
    }
    [SerializeField]
    private Rigidbody2D rb;
    private const float PlayerMovementSpeedTemp = 0.9f;
    private const float ResFactor = 0.3f;
    private const float PlayerMaxSpeed = 2.6f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        MovementDir =
            (Input.GetKey(KeyCode.W) ? 1 : 0) * Vector2.up +
            (Input.GetKey(KeyCode.S) ? 1 : 0) * Vector2.down +
            (Input.GetKey(KeyCode.D) ? 1 : 0) * Vector2.right +
            (Input.GetKey(KeyCode.A) ? 1 : 0) * Vector2.left;
        Velocity += MovementDir.normalized * PlayerMovementSpeedTemp;
        if (Velocity != Vector2.zero)
        {
            resistance = -Velocity.normalized * ResFactor;
        }
        else
        {
            resistance = Vector2.zero;
        }
        if (MovementDir == Vector2.zero && velocity.sqrMagnitude < resistance.magnitude)
        {
            Velocity = Vector2.zero;
        }
        else
        {
            Velocity += resistance;
        }
        rb.velocity = Velocity;
    }
}
