using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public float speed = 2f; // Movement speed of the enemy
    public float detectionRadius = 5f; // Radius within which the player can be detected
    public float patrolRadius = 5f; // Radius within which the enemy will patrol
    public Transform playerTransform; // Reference to the player's transform
    public Transform patrolPointA; // Reference to the first patrol point
    public Transform patrolPointB; // Reference to the second patrol point

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isChasing = false;
    private bool isPatrolling = true;
    private Transform currentPatrolPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentPatrolPoint = patrolPointB.transform;
    }

    void Update()
    {
        // Check if the player is within the detection radius
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        if (distance <= detectionRadius)
        {
            isChasing = true;
            isPatrolling = false;
        }
        else
        {
            isChasing = false;
            isPatrolling = true;
        }

        // Move the enemy
        if (isChasing)
        {
            ChasePlayer();
            FlipSprite();
        }
        else if (isPatrolling)
        {
            PatrolArea();
            FlipSprite();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void ChasePlayer()
    {
        // Calculate the direction towards the player and move the enemy
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    void PatrolArea()
    {
        // Move the enemy back and forth between the patrol points
        Vector2 direction = (currentPatrolPoint.position - transform.position).normalized;
        rb.velocity = direction * speed;

        // Switch to the other patrol point when the enemy reaches the current one
        if (Vector2.Distance(transform.position, currentPatrolPoint.position) < 0.5f)
        {
            if (currentPatrolPoint == patrolPointB.transform)
            {
                currentPatrolPoint = patrolPointA.transform;
            }
            else
            {
                currentPatrolPoint = patrolPointB.transform;
            }
        }
    }

    void FlipSprite()
    {
        // Flip the sprite based on the direction of movement
        if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void OnDrawGizmos()
    {
        // Draw the detection and patrol radii in the Unity editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);

        // Draw the patrol points and the line between them
        if (patrolPointA != null && patrolPointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(patrolPointA.position, 0.5f);
            Gizmos.DrawWireSphere(patrolPointB.position, 0.5f);
            Gizmos.DrawLine(patrolPointA.position, patrolPointB.position);
        }
    }
}