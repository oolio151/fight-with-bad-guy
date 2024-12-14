using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    [Header("Movement")]
    public float playerSpeed = 5f;
    public float jumpForce = 5f;
    public float groundCheckDistance = 0.5f;

    [Header("Physics")]
    private Rigidbody rb;
    private bool isGrounded;

    private Vector3 moveDirection;

    private void Start()
    {
        if (!IsOwner) return;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (!IsOwner) return;

        HandleInput();
        CheckGrounded();
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;

        MovePlayer();
    }

    private void HandleInput()
    {
        // Movement inputs using Keyboard.current
        float moveX = 0f;
        float moveZ = 0f;

        if (Keyboard.current.wKey.isPressed) moveZ = 1f;    // Forward
        if (Keyboard.current.sKey.isPressed) moveZ = -1f;   // Backward
        if (Keyboard.current.dKey.isPressed) moveX = 1f;    // Right
        if (Keyboard.current.aKey.isPressed) moveX = -1f;   // Left

        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Jump Input
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        // Apply movement
        Vector3 moveVector = transform.forward * moveDirection.z + transform.right * moveDirection.x;
        rb.AddForce(moveVector * playerSpeed, ForceMode.Force);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f);
    }
}
