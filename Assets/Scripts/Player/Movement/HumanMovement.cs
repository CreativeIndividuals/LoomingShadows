using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
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
    [SerializeField] float MoveSpeed=5f;
    float horizontal;

    [Space]
    [Header("Jump Values")]
    [SerializeField] float JumpStrength;

    [Space]
    [Header("dash Values")]
    [SerializeField] float dashDuration=0.2f;
    [SerializeField] float dashCooldown=1f;
    [SerializeField] float sdashCooldown=2f;
    [SerializeField] float dashPower=24f;
    [SerializeField] float sdashPower=16f;
    [SerializeField] float sdashCancelTimerCheck=0.1f;
    [SerializeField] TrailRenderer tr;

    bool isFacingRight = true;
    bool isSdashing=false;
    bool sdashCanceled=false;
    bool unlockedDbJump;
    bool unlockedDash;
    bool unlockedSdash;
    bool canDash=true;
    bool canSdash=false;
    bool cantMove=false;
    bool grounded=false;
    bool dbjumped=false;
    private void Update()
    {
        if (cantMove)
            return;

        #region Walk
        if(Input.GetKey(KeyCode.RightArrow))horizontal=1f;
        else if(Input.GetKey(KeyCode.LeftArrow))horizontal=-1f;
        else horizontal=0f;
        //metroidvanias and platformers don't use wasd
        DesiredXVelocity = horizontal * MoveSpeed;

        if (horizontal != 0)
            xVel = Mathf.Lerp(xVel, DesiredXVelocity, Acceleration * Time.deltaTime);
        else
            xVel = Mathf.MoveTowards(xVel, 0f, Friction * Time.deltaTime);
        rb.velocity = new Vector2 (xVel, rb.velocity.y);
        AnimatorMap.instance.setVar("speed",Mathf.Abs(xVel));
        #endregion

        #region dash
        if (unlockedDash && Input.GetKeyDown(GameState.instance.currentState.settings.keyBinds.Dash)&&canDash)
        {
            AnimatorMap.instance.setVar("dashing",true);//this both sets the var and passes it to animator
            StartCoroutine(Dash());
        }
        #endregion

        #region sdash
        if (unlockedSdash && grounded && canSdash && Input.GetKeyDown(GameState.instance.currentState.settings.keyBinds.Sdash))
        {
            AnimatorMap.instance.setVar("sdashing",true);//this both sets the var and passes it to animator
        }
        #endregion

        #region Jump
        if (rb.velocity.y > 0f && Input.GetKeyUp(GameState.instance.currentState.settings.keyBinds.Jump))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if (Input.GetKeyDown(GameState.instance.currentState.settings.keyBinds.Jump)){//optimization and dbjump
            if(isSdashing) {//jumping cancels sdash
                AnimatorMap.instance.setVar("dashing",false);//both sets the var and passes it to animator
                sdashCanceled=true;
                return;
            }
            if(grounded){
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * JumpStrength, ForceMode2D.Impulse);
                AnimatorMap.instance.setVar("jumping",true);//this both sets the var and passes it to animator
            }else if(unlockedDbJump && !dbjumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * JumpStrength, ForceMode2D.Impulse);
                AnimatorMap.instance.setVar("jumping",false);
                AnimatorMap.instance.setVar("dbjumping",dbjumped=true);//this both sets the var and passes it to animator
            }
        }
        #endregion
        Flip();
    }
    public void PauseMovement(float duration){//to use from human combat
        cantMove=true;
        Invoke(nameof(UnPauseMovement),duration);
    }
    public void PauseMovement(){//to use from human combat
        cantMove=true;
    }
    public void UnPauseMovement(){
        cantMove=false;
    }
    private void Start()
    {
        var storyItems = GameState.instance.currentState.foundStoryItems;
        unlockedDbJump = storyItems.Contains(Structs.storyItems.wings);
        unlockedDash = storyItems.Contains(Structs.storyItems.dash);
        unlockedSdash = storyItems.Contains(Structs.storyItems.superdash);
    }

    private void FixedUpdate()
    {
        groundCheck();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        sdashCanceled=true;
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    IEnumerator Dash(){
        canDash = false;
        cantMove = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        tr.emitting = false;
        cantMove = false;
        AnimatorMap.instance.setVar("dashing",false);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    IEnumerator SDash(){
        canSdash = false;
        cantMove = true;
        sdashCanceled=false;
        isSdashing=true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * sdashPower, 0f);
        tr.emitting = true;
        while (!sdashCanceled)
        {
            yield return new WaitForSeconds(sdashCancelTimerCheck);
        }
        isSdashing=false;
        rb.gravityScale = originalGravity;
        tr.emitting = false;
        cantMove = false;
        AnimatorMap.instance.setVar("dashing",false);
        yield return new WaitForSeconds(sdashCooldown);
        canSdash = true;
    }
    public void groundCheck()
    {
        grounded=Physics2D.CapsuleCast(_CapsuleCollider.transform.position, _CapsuleCollider.bounds.size, _CapsuleCollider.direction = CapsuleDirection2D.Vertical, 0f, Vector2.down, 0.05f, GroundLayer);
        if (grounded)
        {
            dbjumped=false;
            AnimatorMap.instance.setVar("jumping",false);
        }
    }
}
