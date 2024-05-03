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
    Vector2 m_moveInput = Vector2.zero;
    Animator m_animator;
    public float interactDistance = 2f; // The distance at which the player can interact with the tribe leader
    private GameObject tribeLeader; // Reference to the tribe leader GameObject
    public AudioSource dash;

    private RespawnManager respawnManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        OnMove();

        respawnManager = FindObjectOfType<RespawnManager>();

        // Check if the RespawnManager was found
        if (respawnManager == null)
        {
            Debug.LogError("RespawnManager not found in the scene!");
        }
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Check for dash input (Space button)
        if (Input.GetButtonDown("Jump") && !isDashing)
        {
            dash.Play();
            StartCoroutine(Dash());
        }

        FlipSprite();

        // Check for interaction with the tribe leader

        OnMove(); // Call the OnMove method here
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

        m_moveInput.x = Input.GetAxisRaw("Horizontal");
        m_moveInput.y = Input.GetAxisRaw("Vertical");

        if (m_moveInput != Vector2.zero)
        {
            m_animator.SetFloat("xMove", m_moveInput.x);
            m_animator.SetFloat("yMove", m_moveInput.y);
            CreateDust();
        }
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

        void CreateDust()
        {
            dust.Play();

        }
    }
}