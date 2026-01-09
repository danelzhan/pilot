using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public PlayerRole role;

    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Only the owning client can control this player
        if (!IsOwner) return;

        moveInput.x = Input.GetAxisRaw("Horizontal"); // A / D
        moveInput.y = Input.GetAxisRaw("Vertical");   // W / S
        moveInput = moveInput.normalized;
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;

        rb.linearVelocity = moveInput * moveSpeed;
    }

}