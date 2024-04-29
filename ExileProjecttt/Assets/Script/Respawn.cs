using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Vector2 startPos;
    SpriteRenderer spriteRenderer;
    public PlayerHealth playerHealth;
    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerHealth.health == 0f)
        {
            Die();
        }
    }
    void Die()
    {
        StartCoroutine(Respawnn(0.5f));
    }
    IEnumerator Respawnn(float duration)
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
        spriteRenderer.enabled = true;
    }
}