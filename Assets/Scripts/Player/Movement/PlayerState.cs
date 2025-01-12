using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerState : MonoBehaviour
{//for getting and switching the player state ONLY(encapsulation)
    public static playerState instance;
    [Header("State")]
    public Structs.PlayerState state;
    private void Awake() {
        if (instance!=null && instance!=this)
        {
            Destroy(this);
        }else{
            instance=this;
        }
    }

    public void SwitchState(Structs.PlayerState newState)
    {
        state = newState;
        if (state == Structs.PlayerState.shadow)
        {
            if(shadowController != null)
                playerAnimator.runtimeAnimatorController = shadowController;
        }
        else if (state == Structs.PlayerState.human)
        {
            if(HumanController != null)
                playerAnimator.runtimeAnimatorController = HumanController;
        }
    }
 
}

