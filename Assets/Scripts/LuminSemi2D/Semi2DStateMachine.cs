using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Semi2DMovement))]
public class Semi2DStateMachine : MonoBehaviour
{
    private Animator animator;
    private Semi2DMovement movement;
    private string lastDirection = "Right";
    private bool isSleeping = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<Semi2DMovement>();
    }

    void Update()
    {
        Vector2 moveInput = movement.GetMoveInput();
        float magnitude = moveInput.magnitude;

        // Wake up if moving
        if (isSleeping && magnitude > 0.01f)
        {
            isSleeping = false;
            animator.Play("Idle" + lastDirection);
            return;
        }

        // Movement handling
        if (magnitude > 0.01f)
        {
            string direction = GetDirection(moveInput);
            animator.Play("Walk" + direction);
            lastDirection = direction;
            return;
        }

        // Idle state
        if (!isSleeping)
        {
            animator.Play("Idle" + lastDirection);
        }
    }

    private string GetDirection(Vector2 moveInput)
    {
        if (Mathf.Abs(moveInput.x) >= Mathf.Abs(moveInput.y))
            return moveInput.x > 0 ? "Right" : "Left";

        return moveInput.y > 0 ? "Up" : "Down";
    }

    public void EnterSleep()
    {
        isSleeping = true;
        animator.Play("SemiSleep");
    }
}