using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public AudioSource Munch;
    public PlayerHealth playerHealth;
    private float HP = 10;
    public PlayerMovement playerMovement;
    private float S = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerMovement.moveSpeed += S;
            playerHealth.health += HP;
            Destroy(gameObject);
            Munch.Play();
        }
    }
}
