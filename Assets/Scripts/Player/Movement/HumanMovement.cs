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
    bool jump = false;

    private void Update()
    {
        if (!Active)
            return;

        #region Walk
        horizontal = Input.GetAxisRaw("Horizontal");
        DesiredXVelocity = horizontal * MoveSpeed;

        if (horizontal != 0)
            xVel = Mathf.Lerp(xVel, DesiredXVelocity, Acceleration * Time.deltaTime);
        else
            xVel = Mathf.MoveTowards(xVel, 0f, Friction * Time.deltaTime);
        rb.velocity = new Vector2 (xVel, rb.velocity.y);
        #endregion

        #region Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            jump = true;
        #endregion
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
    }

    public bool IsGrounded()
    {
        return Physics2D.CapsuleCast(_CapsuleCollider.transform.position, _CapsuleCollider.bounds.size, _CapsuleCollider.direction = CapsuleDirection2D.Vertical, 0f, Vector2.down, 0.05f, GroundLayer);
    }
}
