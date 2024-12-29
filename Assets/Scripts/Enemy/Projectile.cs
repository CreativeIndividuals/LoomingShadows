using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 5.0f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 knockbackDirection = collision.transform.position - transform.position;
            collision.GetComponent<Health>().Dammage(damage);
            collision.GetComponent<Health>().ApplyKnockback(knockbackDirection);
            Destroy(gameObject);
        }
    }
}