using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public bool Active = false;
    [Space]
    Vector2 mousePos;
    Vector2 direction;
    [SerializeField] float Speed;

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - new Vector2(transform.position.x, transform.position.y);
    }

    private void FixedUpdate()
    {
        rb.AddForce(direction.normalized * Speed, ForceMode2D.Force);
    }
}
