using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    float moveSpeed = 5f;
    bool isFacingRight = true;

    Rigidbody2D rb;

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
        // Create a new vector for movement based on horizontal and vertical inputs
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
        rb.velocity = movement;
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
        if (isFacingRight)
        {
            transform.localScale = new Vector3(2, 2, 1); // Adjust the scale values as needed
        }
        else
        {
            transform.localScale = new Vector3(-2, 2, 1); 
        }
    }
}