using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public ParticleSystem dust;
    float horizontalInput;
    float verticalInput;
    public float moveSpeed = 5f;
    bool isFacingRight = true;
    Rigidbody2D rb;
    Vector2 velocity = Vector2.zero;
    float dashDuration = 0.5f;
    bool isDashing = false;
    Vector2 moveInput = Vector2.zero;
    Animator animator;
    public float interactDistance = 2f;
    private GameObject tribeLeader;
    public AudioSource dashSound; // Reference to the dash sound
    private RespawnManager respawnManager;
    public float attackAnimationDuration = 0.5f;
    public AudioSource Walk;

    public Animator attack;

    private float lastAttackTime = 0f;
    public float attackCooldown = 1f; // Adjust this value to change the cooldown duration
    public float attackDamage = 10f; // Player's attack damage

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        OnMove();
        respawnManager = FindObjectOfType<RespawnManager>();
        attack = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && !isDashing)
        {
            dashSound.Play(); // Play the dash sound
            StartCoroutine(Dash());
        }

        FlipSprite();
        OnMove();

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.1f);
    }

    void FlipSprite()
    {
        if (horizontalInput < 0)
        {
            isFacingRight = false;
        }
        else if (horizontalInput > 0)
        {
            isFacingRight = true;
        }

        transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
    }

    IEnumerator Dash()
    {
        CreateDust();
        isDashing = true;
        float startTime = Time.time;
        float endTime = startTime + dashDuration;
        float originalMoveSpeed = moveSpeed;
        moveSpeed *= 2;

        while (Time.time < endTime)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
            yield return null;
        }

        moveSpeed = originalMoveSpeed;
        isDashing = false;
    }

    void OnMove()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("xMove", moveInput.x);
            animator.SetFloat("yMove", moveInput.y);
            CreateDust();
            Walk.Play();
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check for collisions with objects tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            attack.SetBool("isAttacking", true);

            // Get a reference to the Enemy script
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Get the enemy's attack damage
                float enemyAttackDamage = enemy.GetAttackDamage();

                // Get a reference to the PlayerHealth script
                PlayerHealth playerHealth = GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    // Reduce the player's health
                    playerHealth.UpdateHealth(-enemyAttackDamage);

                    // Check if the player's health is zero or less
                    if (playerHealth.health <= 0)
                    {
                        HandlePlayerDeath();
                    }
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            attack.SetBool("isAttacking", false);
        }
    }

    private void HandlePlayerDeath()
    {
        // Call the RespawnPlayer method from the RespawnManager
        respawnManager.RespawnPlayer();
    }

    public void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            StartCoroutine(AttackAnimation());
        }
    }

    IEnumerator AttackAnimation()
    {
        animator.SetBool("isAttacking", true);
        // Add any additional attack logic here, such as handling cooldowns or applying damage

        // Wait for the attack animation to finish
        yield return new WaitForSeconds(attackAnimationDuration);

        animator.SetBool("isAttacking", false);
    }
}