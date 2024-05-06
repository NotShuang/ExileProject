using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioSource Ouch;
    public float speed = 3f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    private float canAttack;
    private Transform target;
    private PlayerHealth playerHealth; // Reference to the PlayerHealth component

    void Update()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Get a reference to the PlayerHealth component
            if (playerHealth == null)
            {
                playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            }

            // Check if the player's health is not already 0
            if (playerHealth.health > 0)
            {
                if (attackSpeed <= canAttack)
                {
                    playerHealth.UpdateHealth(-attackDamage);
                    Ouch.Play();
                    canAttack = 0f;
                }
                else
                {
                    canAttack += Time.deltaTime;
                }
            }
        }
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
}