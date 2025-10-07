using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Move,
        Jump,
        Fall,
        Hurt,
        Death,
        ATK,
        Sleep
    }
    
    private PlayerState currentState;
    
    private Dictionary<PlayerState, string> animatorStates = new Dictionary<PlayerState, string>();
    
    private Animator animator;
    
    private PlayerMovement playerMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        animatorStates = new Dictionary<PlayerState, string>
        {
            { PlayerState.ATK, "ATK" },
            { PlayerState.Death, "Death" },
            { PlayerState.Fall, "Fall" },
            { PlayerState.Hurt, "Hurt" },
            { PlayerState.Idle, "Idle" },
            { PlayerState.Jump, "Jump" },
            { PlayerState.Move, "Move" },
            { PlayerState.Sleep, "Sleep" }
        };
        
        ChangeState(PlayerState.Idle);
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState == newState) return;
        
        currentState = newState;
        animator.Play(animatorStates[currentState]);
    }
    
    private void Update()
    {
        PlayerState newState = GetNexState();
        
        if(newState != currentState) ChangeState(newState);
    }

    private PlayerState GetNexState()
    {
        PlayerState highPriorityState = CheckHighPriorityState();
        if (highPriorityState != PlayerState.Idle) return highPriorityState;
        return PlayerState.Idle;
    }

    private PlayerState CheckHighPriorityState()
    {
        if (playerMovement.isDead) return PlayerState.Death;
        if (playerMovement.isHurt) return PlayerState.Hurt;
        return playerMovement.isGrounded ? GetGroundedState() : GetAirborneState();
    }

    private PlayerState GetGroundedState()
    {
        if (playerMovement.isAttacking) return PlayerState.ATK;
        if (playerMovement.isMoving) return PlayerState.Move;
        return PlayerState.Idle;
    }

    private PlayerState GetAirborneState()
    {
        return playerMovement.isFalling ? PlayerState.Fall : PlayerState.Jump;
    }
    
    public void EndAttackState()
    {
        if(playerMovement.isAttacking || playerMovement.isShooting)
        {
            playerMovement.isAttacking = false;
            playerMovement.isShooting = false;
        }
    }
}