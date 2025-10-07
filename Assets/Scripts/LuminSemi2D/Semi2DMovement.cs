using UnityEngine;
using UnityEngine.InputSystem;

public class Semi2DMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }
}