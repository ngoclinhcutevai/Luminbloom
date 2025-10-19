using UnityEngine;

public class IdleSleepHandler : MonoBehaviour
{
    public PlayerStateMachine playerStateMachine;
    public PlayerMovement playerMovement;

    [SerializeField] private float idleThreshold = 300f; // seconds before sleep
    private float idleTimer;
    private bool isSleeping;

    void Start()
    {
        // Auto-assign if not set in Inspector
        if (playerStateMachine == null)
            playerStateMachine = GetComponent<PlayerStateMachine>();
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        bool anyInput =
            Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 ||
            Input.GetKey(KeyCode.Space) ||
            Input.GetKey(KeyCode.J);

        if (anyInput)
        {
            idleTimer = 0f;
            if (isSleeping)
            {
                isSleeping = false;
                playerStateMachine.ChangeState(PlayerStateMachine.PlayerState.Idle);
            }
        }
        else
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleThreshold && !isSleeping)
            {
                isSleeping = true;
                playerStateMachine.ChangeState(PlayerStateMachine.PlayerState.Sleep);
            }
        }
    }
}