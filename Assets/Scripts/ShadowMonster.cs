using UnityEngine;

public class ShadowMonster : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float speed = 2.0f; // Movement speed
    private Rigidbody2D rb;
    private Vector2 movement;

    // Light Detection
    private bool isInLight = false; // Whether the monster is in light

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Find the player automatically if not set
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (!isInLight) // Only move when NOT in light
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;
        }
        else
        {
            movement = Vector2.zero; // Stop movement in light
        }
    }

    private void FixedUpdate()
    {
        if (!isInLight)
        {
            MoveMonster(movement);
        }
    }

    void MoveMonster(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.fixedDeltaTime));
    }

    // Light Detection
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LightZone"))
        {
            isInLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LightZone"))
        {
            isInLight = false;
        }
    }
}
