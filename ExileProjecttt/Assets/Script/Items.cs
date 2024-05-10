using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public AudioSource munchtSound; // Reference to the munch sound
    public PlayerHealth playerHealth;
    private float healthBonus = 10f;
    public PlayerMovement playerMovement;
    private float speedBonus = 3f;
    public GameObject soundSourceGameObject;

    // Reference to the particle system
    public ParticleSystem explode;

    void Start()
    {
        // Get the AudioSource component from the assigned GameObject
        munchtSound = soundSourceGameObject.GetComponent<AudioSource>();

        // Get the ParticleSystem component from this game object
        explode = GetComponent<ParticleSystem>();

        // Check if the ParticleSystem component exists
        if (explode == null)
        {
            Debug.LogWarning("ParticleSystem component not found on this game object.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Check if the ParticleSystem component exists before trying to play it
            if (explode != null)
            {
                explode.Play();
            }

            Debug.Log("Entering trigger");
            munchtSound.Play(); // Play the munch sound
            Debug.Log("After playing sound");
            playerMovement.moveSpeed += speedBonus; // Increase player speed
            playerHealth.UpdateHealth(healthBonus); // Increase player health
            Destroy(gameObject); // Destroy the apple object
        }
    }
}