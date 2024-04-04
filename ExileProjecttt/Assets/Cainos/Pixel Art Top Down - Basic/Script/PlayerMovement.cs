using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    float moveSpeed = 5f;
    bool isFacingRight = true;
    Rigidbody2D rb;

    // New variable to store the player's velocity
    Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        FlipSprite();
    }

    private void FixedUpdate()
    {
        // Create a new vector for target velocity based on horizontal and vertical inputs
        Vector2 targetVelocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);

        // Use SmoothDamp to interpolate the player's velocity toward the target velocity
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.1f);
    }

    void FlipSprite()
    {
        // Check if the player is moving horizontally
        if (horizontalInput != 0)
        {
            isFacingRight = horizontalInput > 0;
        }
        // Check if the player is moving vertically
        else if (verticalInput != 0)
        {
            isFacingRight = verticalInput > 0;
        }

        // Flip the sprite based on the direction
        transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
    }
}