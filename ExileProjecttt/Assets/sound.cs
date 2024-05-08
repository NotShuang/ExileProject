using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    public AudioSource Walk;
    Rigidbody2D rb2D;
    float x;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
       Walk = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       x = Input.GetAxis("Horizontal"); 
       
        
        if (rb2D.velocity.x != 0)
        {
            if (!Walk.isPlaying)
            {
                Walk.Play();
            }
        }
        else
        {
            Walk.Stop();
        }
    }
}
