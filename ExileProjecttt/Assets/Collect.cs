using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Collect : MonoBehaviour
{
    public AudioSource Audio;

    private void Update()
    {
        
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Audio.Play();
            Destroy(gameObject);
        }
    }
}

