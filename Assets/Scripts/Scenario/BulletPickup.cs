using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    // It controls the bullet pickups, and it passes on the new bullet properties: new bullet GO,
    // time between shots, number of bullets available, index of the bullet type.

    [SerializeField] GameObject bulletAssociated;
    [SerializeField] float timeBetweenShots = 1;
    [SerializeField] int numBullets = 10;
    [SerializeField] int bulletIndex = 1;

    PlayerShooting playerShooting;

    
    void Start()
    {
        playerShooting = FindObjectOfType<PlayerShooting>();
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Passess on the bullet characteristics and self destroys
            playerShooting.SetCurrentBulletProperties(bulletAssociated, timeBetweenShots, numBullets,
                bulletIndex);
            other.GetComponent<PlayerStats>().PlayPickupSound();
            Destroy(gameObject);
        }
    }
}
