using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public bool Active = true;

    [Space]
    [Header("Physics Values")]
    [SerializeField] Vector2 DesiredVelocity;
    [SerializeField] float Acceleration;
    [SerializeField] float Friction;

    [Space]
    [Header("Moving Values")]
    [SerializeField] float MoveSpeed;
    float horizontal;
    float vertical;
    bool canMove = true;

    [Space]
    [Header("Roll Values")]
    [SerializeField] float RollStrength;
    [SerializeField] float RollDuration;
    bool canRoll = true;
    bool roll;

    Vector2 direction;
    Vector2 lastDirection;

    private void Update()
    {
        if (!Active)
            return;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector2(horizontal, vertical).normalized;
        if (direction != Vector2.zero && canMove) lastDirection = direction;

        DesiredVelocity = direction * MoveSpeed;

        if (canMove){
            if (direction != Vector2.zero)
                rb.velocity = Vector2.Lerp(rb.velocity, DesiredVelocity, Acceleration * Time.deltaTime);
            else
                rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, Friction * Time.deltaTime);
        }
            
        if (Input.GetKeyDown(KeyCode.Space) && canRoll)
        {
            canMove = false;
            roll = true;
            canRoll = false;
            Invoke(nameof(StopRoll), RollDuration);
        }

        if (roll)
        {
            rb.velocity = lastDirection * RollStrength;
        }
    }

    void StopRoll()
    {
        canMove = true;
        roll = false;
        canRoll = true;
    }
}
