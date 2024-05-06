using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    [SerializeField] private float maxHealth = 100f;
    public Slider healthSlider;

    private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    private Vector3 initialSpawnPosition; // Initial spawn position of the player

    public AudioSource Ouch;
    private void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;

        // Find the RespawnManager in the scene
        playerMovement = GetComponent<PlayerMovement>();

        initialSpawnPosition = transform.position;
    }

    private void Update()
    {
        // No need to update the healthSlider value here
    }

    public void UpdateHealth(float mod)
    {
        health += mod;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0f)
        {
            Ouch.Play();
            health = 0f;
            Debug.Log("Player Respawn");
            RespawnPlayer();
        }

        // Update the healthSlider value
        healthSlider.value = health;
    }

    private void RespawnPlayer()
    {
        // Reset the player's position to the initial spawn position
        transform.position = initialSpawnPosition;

        // Reset the player's health to the maximum value
        health = maxHealth;

        // Reset any other player-related variables or components here
        // ...
    }
}