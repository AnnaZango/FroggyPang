using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour, ICollectable
{
    // It controls the properties of the player's shield, by passing on the points of the shield
    // upon colliding with player. Shield pickups have a bouncy material which makes them jump when
    // colliding with something, making it more difficult for the player to grab it

    [SerializeField] int shieldPoints = 1; // 1 or 2
    Shield shield;
    PlayerPoints playerPoints;
    int maxPointsShield = 2;

    private void Awake()
    {
        shield = FindObjectOfType<Shield>();
        playerPoints = FindObjectOfType<PlayerPoints>();
    }

    public void Collect()
    {
        int currentShieldPoints = shield.GetCurrentShieldPoints();
        if(currentShieldPoints == maxPointsShield) { return; }
        shield.GainShieldPoints(shieldPoints);
        playerPoints.PlayPickupSound();
        Destroy(gameObject);
    }
    
}
