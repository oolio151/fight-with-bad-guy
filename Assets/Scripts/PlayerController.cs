using UnityEngine;
#if NEW_INPUT_SYSTEM_INSTALLED
using UnityEngine.InputSystem;
#endif
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    [Header("Movement")]
    public float Speed = 5f;
    public float JumpForce = 5f;
    public float GroundCheckDistance = 0.5f;

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        if (!IsOwner) return;

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on the player object.");
        }
    }

    private void Update()
    {
        // Ensure only the owner can control this player
        if (!IsOwner || !IsSpawned) return;

        CheckGrounded();
    }

    private void FixedUpdate()
    {
        // Ensure only the owner can control this player
        if (!IsOwner || rb == null) return;

        HandleMovement();
    }

    private void HandleMovement()
    {
        float multiplier = Speed;
        Vector3 moveDirection = Vector3.zero;

#if ENABLE_INPUT_SYSTEM && NEW_INPUT_SYSTEM_INSTALLED
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed)
            {
                moveDirection += Vector3.left;
            }
            if (Keyboard.current.dKey.isPressed)
            {
                moveDirection += Vector3.right;
            }
            if (Keyboard.current.wKey.isPressed)
            {
                moveDirection += Vector3.forward;
            }
            if (Keyboard.current.sKey.isPressed)
            {
                moveDirection += Vector3.back;
            }

            // Handle jumping
            if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
            {
                Jump();
            }
        }
#else
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.back;
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
#endif

        // Apply movement using AddForce
        rb.AddForce(moveDirection.normalized * multiplier, ForceMode.Force);
    }

    private void Jump()
    {
        // Apply jump force
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        Debug.Log("Jumping");
    }

    private void CheckGrounded()
    {
        // Perform a ground check using Raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, GroundCheckDistance + 0.1f);
        Debug.Log($"IsGrounded: {isGrounded}");
    }
}
