using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class SemiSleepHandler : MonoBehaviour
{
    [SerializeField] public float idleThreshold = 5f;
    private float idleTimer = 0f;

    private Animator animator;
    private Semi2DStateMachine stateMachine;

    void Awake()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<Semi2DStateMachine>();
    }

    void Update()
    {
        bool isMoving = Keyboard.current.wKey.isPressed ||
                        Keyboard.current.aKey.isPressed ||
                        Keyboard.current.sKey.isPressed ||
                        Keyboard.current.dKey.isPressed;

        if (isMoving)
        {
            idleTimer = 0f; // reset timer
        }
        else
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleThreshold)
            {
                animator.Play("SemiSleep");
            }
        }
    }
}