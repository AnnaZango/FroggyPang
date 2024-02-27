using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    // It controls the properties of the player's shield, by passing on the health of the shield
    // upon colliding with player. Shield pickups have a bouncy material which makes them jump when
    // colliding with something, making it more difficult for the player to grab it

    [SerializeField] int shieldHealth = 1; // 1 or 2

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().ActivateShield(shieldHealth);
            Destroy(gameObject);
        }
    }
}
