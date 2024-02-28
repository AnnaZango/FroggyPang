using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleElement : MonoBehaviour
{
    //Script used for destructible platforms, which are destroyed when colliding with bullet.

    [SerializeField] GameObject particles;
    [SerializeField] GameObject pickup;
    [SerializeField] AudioSource soundDestruction;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "bullet")
        {            
            Instantiate(particles, transform.position, Quaternion.identity);
            soundDestruction.Play();
            if (pickup != null)
            {
                Instantiate(pickup, transform.position, Quaternion.identity);
            }            
            Destroy(gameObject);
        }
    }
}
