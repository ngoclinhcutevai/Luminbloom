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
        
        if (isSleeping && moveInput.magnitude > 0.01f)
        {
            isSleeping = false;
            animator.Play("Idle" + lastDirection);
            return;
        }

        if (moveInput.magnitude > 0.01f)
        {
            // Walking
            if (Mathf.Abs(moveInput.x) >= Mathf.Abs(moveInput.y))
            {
                if (moveInput.x > 0)
                {
                    animator.Play("WalkRight");
                    lastDirection = "Right";
                }
                else
                {
                    animator.Play("WalkLeft");
                    lastDirection = "Left";
                }
            }
            else
            {
                if (moveInput.y > 0)
                {
                    animator.Play("WalkUp");
                    lastDirection = "Up";
                }
                else
                {
                    animator.Play("WalkDown");
                    lastDirection = "Down";
                }
            }
        }
        else if (!isSleeping)
        {
            animator.Play("Idle" + lastDirection);
        }
    }
    
    public void EnterSleep()
    {
        isSleeping = true;
        animator.Play("SemiSleep");
    }
}