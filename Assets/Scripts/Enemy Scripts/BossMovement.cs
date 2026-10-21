using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 300f;
    [SerializeField] private float minChangeTime = 10f;
    [SerializeField] private float maxChangeTime = 15f;

    [Header("Combat Settings")]
    [SerializeField] private float dashCooldown = 8f;
    private bool isOnDashCooldown;

    private Rigidbody2D rb;
    private Vector2 movementDirection = new Vector2(1, 0).normalized;
    private float directionChangeTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Ensure gravity doesn't pull the top-down enemy down
        rb.gravityScale = 0f; 
        
        DashAttack();
    }

    void Update()
    {
        if (isOnDashCooldown == false) {
                DashAttack();
            }
    }

    void FixedUpdate()
    {
        // Apply physics movement in FixedUpdate for smooth collision interactions
        //movementDirection.x = movementDirection.x * (moveSpeed * Time.deltaTime);
        rb.linearVelocity = movementDirection * (moveSpeed * Time.deltaTime);
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
            Debug.Log("hit wall");
            if (GetComponent<SpriteRenderer>().flipX == true){
                GetComponent<SpriteRenderer>().flipX = false;
            } else {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (isOnDashCooldown == false) {
                DashAttack();
            }
        }
    }

    private IEnumerator DashAttackCooldownRoutine()
    {
        isOnDashCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        isOnDashCooldown = false;
    }

    private void DashAttack()
    {
        movementDirection = movementDirection * -1;
        StartCoroutine(DashAttackCooldownRoutine());
        // Reset the timer to a random interval
        directionChangeTimer = Random.Range(minChangeTime, maxChangeTime);
    }
}
