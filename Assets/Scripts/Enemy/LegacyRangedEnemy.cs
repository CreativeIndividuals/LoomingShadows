using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegacyRangedEnemy : BaseEnemy
{
    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootInterval = 2.0f;
    public float projectileSpeed = 5.0f;

    [Header("Detection Settings")]
    public float detectionRange = 10.0f;

    [Header("Movement Settings")]
    public float moveSpeed = 2.0f;
    public float stopDistance = 5.0f;

    private Transform player;
    private float lastShootTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            RotateTowardsPlayer();
            TryShoot();
            if (distanceToPlayer > stopDistance)
            {
                MoveTowardsPlayer();
            }
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void TryShoot()
    {
        if (Time.time - lastShootTime >= shootInterval)
        {
            lastShootTime = Time.time;
            ShootProjectile();
            // animation goes here
        }
    }

    private void ShootProjectile()
    {
        if (firePoint == null || projectilePrefab == null) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePoint.right * projectileSpeed;
        }
    }
}
