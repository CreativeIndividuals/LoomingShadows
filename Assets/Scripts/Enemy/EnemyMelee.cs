using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3.0f;
    public float stoppingDistance = 1.5f;

    [Header("Attack Settings")]
    public float attackCooldown = 1.0f;
    public int attackDamage = 10;
    
    private Transform player;

    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > stoppingDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
            //animation goes here //moving
        }
        else
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            Invoke(nameof(DealDamage), 0.2f);
            //animation goes here //attack
        }
    }

    public void DealDamage()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= stoppingDistance)
        {
            player.GetComponent<Health>().Dammage(attackDamage);
            Vector2 knockbackDirection = player.position - transform.position;
            player.GetComponent<Health>().ApplyKnockback(knockbackDirection);
        }
    }
}
