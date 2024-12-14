using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float playerSpeed = 1f;
    public float groundDrag;
    public float airMultiplier;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool isGrounded;
    public Transform orientation;
    Vector3 moveDirection;
    Rigidbody rb;

    public float jumpCooldown;
    public float jumpForce = 1f;
    bool readyToJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        HandleInput();

        if (isGrounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = airMultiplier;

        
        moveDirection.y = 0;
        rb.AddForce(moveDirection.normalized * playerSpeed, ForceMode.Force);

        if (isGrounded && Keyboard.current.spaceKey.isPressed && readyToJump)
        {
            Jump();
            Invoke(nameof(ReadyToJump), jumpCooldown);
        }
    }

    private void HandleInput()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        float inputX = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0);
        float inputY = (Keyboard.current.sKey.isPressed ? 1 : 0) - (Keyboard.current.wKey.isPressed ? 1 : 0);

        moveDirection = orientation.forward * inputY - orientation.right * inputX;

        UpdateMoveDirectionServerRpc(moveDirection);
    }

    [ServerRpc]
    private void UpdateMoveDirectionServerRpc(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > playerSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * playerSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        readyToJump = false;
    }

    void ReadyToJump()
    {
        readyToJump = true;
    }
}
