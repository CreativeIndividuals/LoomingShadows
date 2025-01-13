using UnityEngine;

public class LegacyProjectile : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 5.0f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 knockbackDirection = collision.transform.position - transform.position;
            collision.GetComponent<PlayerHealth>().takeDamage(damage);
            collision.GetComponent<PlayerHealth>().ApplyKnockback(knockbackDirection);
            Destroy(gameObject);
        }
    }
}