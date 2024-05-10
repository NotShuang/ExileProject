using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float attackDamage = 10f;
    private float canAttack;
    private Transform target;
    private PlayerHealth playerHealth; // Reference to the PlayerHealth component
    public float EnemyHealth;

    void Update()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }

        // Check enemy health and handle death
        if (EnemyHealth <= 0)
        {
            HandleEnemyDeath();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Get a reference to the PlayerMovement component
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // Check if the player is attacking
                if (playerMovement.attack.GetBool("isAttacking"))
                {
                    // Reduce the enemy's health
                    EnemyHealth -= playerMovement.attackDamage;

                    // Check if the enemy's health is zero or less
                    if (EnemyHealth <= 0)
                    {
                        HandleEnemyDeath();
                    }
                }
            }
        }
    }

    private void HandleEnemyDeath()
    {
        // Add any additional logic for enemy death here, such as playing a death animation or spawning pickups
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
        }
    }

    public float GetAttackDamage()
    {
        return attackDamage;
    }
}