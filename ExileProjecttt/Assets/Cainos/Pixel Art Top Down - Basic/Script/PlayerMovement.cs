using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float dashSpeed = 15f; // Dash speed
    public float dashDuration = 0.2f; // Dash duration in seconds
    public float dashCooldown = 1f; // Dash cooldown in seconds

    private Vector3 moveDirection; // Player's movement direction
    private bool isDashing; // Flag to track if the player is dashing
    private float dashTimer; // Timer for dash duration
    private float dashCooldownTimer; // Timer for dash cooldown

    void Update()
    {
        // Get input from horizontal and vertical axes
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction
        moveDirection = new Vector3(horizontal, 0f, vertical);

        // Dash input
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0f)
        {
            StartDash();
        }

        // Update dash timers
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                StopDash();
            }
        }
        else
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // Move the player
        if (isDashing)
        {
            transform.Translate(moveDirection * dashSpeed * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self);
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;
    }

    void StopDash()
    {
        isDashing = false;
    }
}