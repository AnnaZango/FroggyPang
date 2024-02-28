using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour, ICollectable
{
    // It controls the bullet pickups, and it passes on the new bullet properties: new bullet GO,
    // time between shots, number of bullets available, index of the bullet type.

    [SerializeField] GameObject bulletAssociated;
    [SerializeField] float timeBetweenShots = 1;
    [SerializeField] int numBullets = 10;
    [SerializeField] int bulletIndex = 1;

    PlayerShooting playerShooting;
    PlayerPoints playerPoints;

    private void Awake()
    {
        playerShooting = FindObjectOfType<PlayerShooting>();
        playerPoints = FindObjectOfType<PlayerPoints>();
    }

    public void Collect()
    {
        playerShooting.SetCurrentBulletProperties(bulletAssociated, timeBetweenShots, numBullets,
                bulletIndex);
        playerPoints.PlayPickupSound();
        Destroy(gameObject);
    }
}
