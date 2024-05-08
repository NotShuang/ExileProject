using UnityEngine;
using System.Collections;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        OnMove();
        respawnManager = FindObjectOfType<RespawnManager>();
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
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for collisions with objects tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HandlePlayerDeath();
        }
    }

    private void HandlePlayerDeath()
    {
        // Call the RespawnPlayer method from the RespawnManager
        respawnManager.RespawnPlayer();
    }

    public void Attack()
    {
        StartCoroutine(AttackAnimation());
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