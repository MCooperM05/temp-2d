using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float minChangeTime = 1f;
    [SerializeField] private float maxChangeTime = 2f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private float directionChangeTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Ensure gravity doesn't pull the top-down enemy down
        rb.gravityScale = 0f; 
        
        ChooseNewDirection();
    }

    void Update()
    {
        // Countdown the timer
        directionChangeTimer -= Time.deltaTime;
        if (directionChangeTimer <= 0f)
        {
            ChooseNewDirection();
        }
    }

    void FixedUpdate()
    {
        // Apply physics movement in FixedUpdate for smooth collision interactions
        rb.linearVelocity = movementDirection * moveSpeed;
    }

    private void ChooseNewDirection()
    {
        // Generate a random 2D direction and normalize it
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        movementDirection = new Vector2(randomX, randomY).normalized;

        // Reset the timer to a random interval
        directionChangeTimer = Random.Range(minChangeTime, maxChangeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Instantly switch directions if hitting an obstacle (e.g., walls or tilemaps)
        if (collision.gameObject.CompareTag("Wall"))
        {
            ChooseNewDirection();
        }
    }
}
