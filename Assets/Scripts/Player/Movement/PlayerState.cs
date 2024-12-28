using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState instance;
    [Header("State")]
    public playerState state;

    private Animator playerAnimator;
    
    [Header("Controllers")]
    public RuntimeAnimatorController HumanController;
    public RuntimeAnimatorController shadowController;

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void SwitchState(playerState newState)
    {
        state = newState;
        if (state == playerState.shadow)
        {
            if(shadowController != null)
                playerAnimator.runtimeAnimatorController = shadowController;
        }
        else if (state == playerState.human)
        {
            if(HumanController != null)
                playerAnimator.runtimeAnimatorController = HumanController;
        }
    }


    public enum playerState
    {
        human,
        shadow
    }    
}

