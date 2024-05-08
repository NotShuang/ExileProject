using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    public float EnemyHealth = 30;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            EnemyHealth -= 10f;
        }
        else if(EnemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
