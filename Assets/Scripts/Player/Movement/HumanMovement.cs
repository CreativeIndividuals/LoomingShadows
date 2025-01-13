using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    
    public bool Active = true;

    [Space]
    [Header("Ground Check")]
    [SerializeField] CapsuleCollider2D _CapsuleCollider;
    [SerializeField] LayerMask GroundLayer;

    [Space]
    [Header("Physics Values")]
    [SerializeField] float DesiredXVelocity;
    float xVel;
    [SerializeField] float Acceleration;
    [SerializeField] float Friction;

    [Space]
    [Header("Walk Values")]
    [SerializeField] float MoveSpeed;
    float horizontal;

    [Space]
    [Header("Jump Values")]
    [SerializeField] float JumpStrength;

    [Space]
    [Header("dash Values")]
    [SerializeField] float dashDuration=1f;
    [SerializeField] float dashSpeed;
    [SerializeField] float sdashSpeed;
    bool unlockedDbJump;
    bool unlockedDash;
    bool unlockedSdash;
    bool dbjumped=false;
    bool jump = false;
    bool dash = false;
    bool sdash =false;

    private void Update()
    {
        if (!Active)
            return;

        #region Walk
        horizontal = Input.GetKeyDown(KeyCode.RightArrow)?1:(Input.GetKeyDown(KeyCode.LeftArrow)?-1:0);//metroidvanias and platformers don't use wasd
        DesiredXVelocity = horizontal * MoveSpeed;

        if (horizontal != 0)
            xVel = Mathf.Lerp(xVel, DesiredXVelocity, Acceleration * Time.deltaTime);
        else
            xVel = Mathf.MoveTowards(xVel, 0f, Friction * Time.deltaTime);
        rb.velocity = new Vector2 (xVel, rb.velocity.y);
        AnimatorMap.instance.setVar("speed",xVel);
        #endregion

        #region dash
        if (unlockedDash && Input.GetKeyDown(GameState.instance.currentState.settings.keyBinds.Dash))
        {
            AnimatorMap.instance.setVar("dashing",dash=true);//this both sets the var and passes it to animator
            StartCoroutine(dashEnd());
        }
        #endregion

        #region sdash
        if (unlockedSdash && Input.GetKeyDown(GameState.instance.currentState.settings.keyBinds.Sdash))
        {
            AnimatorMap.instance.setVar("sdashing",sdash=true);//this both sets the var and passes it to animator
        }
        #endregion

        #region Jump
        if(sdash) {//explanation in fixed update
            AnimatorMap.instance.setVar("sdashing",sdash=false);//both sets the var and passes it to animator
            return;
        }
        if (Input.GetKeyDown(GameState.instance.currentState.settings.keyBinds.Jump)){//optimization and dbjump
            if(IsGrounded()){
            AnimatorMap.instance.setVar("jumping",jump=true);//this both sets the var and passes it to animator
            }else if(unlockedDbJump && !dbjumped)
            {
                jump = true;
                AnimatorMap.instance.setVar("jumping",false);
                AnimatorMap.instance.setVar("dbjumping",dbjumped=true);//this both sets the var and passes it to animator
            }
        }
        #endregion
    }
    IEnumerator dashEnd(){
        yield return new WaitForSeconds(dashDuration);
        AnimatorMap.instance.setVar("dashing",false);
    }
    private void Start() {//unlocks check
        unlockedDbJump=GameState.instance.currentState.foundStoryItems.Contains(Structs.storyItems.wings);//unlocked dbjump?
        unlockedDash=GameState.instance.currentState.foundStoryItems.Contains(Structs.storyItems.dash);//unlocked dash?
        unlockedSdash=GameState.instance.currentState.foundStoryItems.Contains(Structs.storyItems.superdash);//unlocked sdash?
    }
    private void FixedUpdate()
    {
        #region Jump
        if (jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * JumpStrength, ForceMode2D.Impulse);
            jump = false;
        }
        #endregion
        #region Dash
        if (dash)
        {
            //TODO:I suck at math plz give em a lil thrust
            dash=false;
        }
        #endregion
        #region Sdash
        if (sdash)
        {
            //TODO:I suck at math plz give em a constant thrust like crystal dash in hollow knight
            //you can use a coroutine i guess but plz remember to cancel it on collision or jump button
            //yeah jump button cancels it
        }
        #endregion
    }

    public bool IsGrounded()
    {
        bool onGround=Physics2D.CapsuleCast(_CapsuleCollider.transform.position, _CapsuleCollider.bounds.size, _CapsuleCollider.direction = CapsuleDirection2D.Vertical, 0f, Vector2.down, 0.05f, GroundLayer);
        if (onGround)
        {
            dbjumped=false;
            AnimatorMap.instance.setVar("jumping",false);
            AnimatorMap.instance.setVar("dbjumping",false);
        } 
        return onGround;
    }
}
