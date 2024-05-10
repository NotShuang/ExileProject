using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    [SerializeField] private float maxHealth = 100f;
    public Slider healthSlider;
    private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    private Vector3 initialSpawnPosition; // Initial spawn position of the player

    private void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        playerMovement = GetComponent<PlayerMovement>();
        initialSpawnPosition = transform.position;
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
            health = 0f;
            Debug.Log("Player Respawn");
            RespawnPlayer();
        }

        healthSlider.value = health;
    }

    private void RespawnPlayer()
    {
        transform.position = initialSpawnPosition;
        health = maxHealth;
    }
}