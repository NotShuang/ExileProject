using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    [SerializeField] private float maxHealth = 100f;
    public Slider healthSlider;

    private RespawnManager respawnManager; // Reference to the RespawnManager

    private void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;

        // Find the RespawnManager in the scene
        respawnManager = FindObjectOfType<RespawnManager>();
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
            health = 0f;
            Debug.Log("Player Respawn");

            // Call the RespawnPlayer method from the RespawnManager
            respawnManager.RespawnPlayer();
        }

        // Update the healthSlider value
        healthSlider.value = health;
    }
}