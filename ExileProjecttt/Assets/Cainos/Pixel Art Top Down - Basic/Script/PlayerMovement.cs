using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    float moveSpeed = 5f;
    bool isFacingRight = true;
    Rigidbody2D rb;
    Vector2 velocity = Vector2.zero;
    float dashDuration = 0.5f;
    bool isDashing = false;

    public Animator animator; 

    public AudioSource dash;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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


    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.1f);
    }

    void FlipSprite()
    {
        if (horizontalInput != 0)
        {
            isFacingRight = horizontalInput > 0;
        }
        else if (verticalInput != 0)
        {
            isFacingRight = verticalInput > 0;
        }

        transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
    }

    IEnumerator Dash()
    {
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


}