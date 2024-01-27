using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    //Controls player shooting mechanics and current bullet type

    [SerializeField] Transform shootingPosition;
    [SerializeField] GameObject tongue;
    [SerializeField] GameObject spit;

    [SerializeField] GameObject currentBullet;

    [SerializeField] float defaultTimeBetweenShots = 1f;
    [SerializeField] GameObject defaultBullet;

    [SerializeField] float timeBetweenShots = 1f; //modifiable depending on bullet type

    [SerializeField] int currentNumBullets = 0;
    [SerializeField] GameObject bulletInfo; //text and image bullet, only active if weapon not default
    [SerializeField] TextMeshProUGUI textBullets;

    [SerializeField] Sprite[] spritesWeapons; //3 different types
    [SerializeField] Image currentWeaponImage; //0 is default

    bool hasShot = false;

    //sounds:
    [SerializeField] AudioSource shootSound;

    void Start()
    {        
        currentBullet = defaultBullet;
        timeBetweenShots = defaultTimeBetweenShots;
        currentWeaponImage.sprite = spritesWeapons[0];
        bulletInfo.SetActive(false);
    }

    
    void Update()
    {
        if (!GameManager.GetPlayerAlive()) { return; }        
    }

   

    public void ShootAction(InputAction.CallbackContext context)
    {
        if (hasShot) { return; }

        GameObject bullet = Instantiate(currentBullet, shootingPosition.position, Quaternion.identity);
        shootSound.Play();
        hasShot = true;

        Invoke(nameof(ResetShooting), timeBetweenShots); //time between shots, like reloading time
        if (currentBullet != defaultBullet) //default type has infinit bullets
        {
            DecreaseNumberOfBullets();
        }
    }

    private void DecreaseNumberOfBullets()
    {
        currentNumBullets--;
        UpdateNumberBullets(); //UI

        if (currentNumBullets <= 0) //back to default
        {
            currentNumBullets = 0;
            currentBullet = defaultBullet;
            currentWeaponImage.sprite = spritesWeapons[0];
            timeBetweenShots = defaultTimeBetweenShots;

            bulletInfo.SetActive(false);
        }
    }

    private void ResetShooting() //allow shooting again
    {
        hasShot = false;
    }

    public void SetCurrentBulletProperties(GameObject bullet, float timeLapseShots, int numBullets,
        int bulletIndex)
    {
        //When the player collides with a bullet pickup, that one gets assigned as current bullet,
        //and it passes on all bullet properties
        currentBullet = bullet;
        timeBetweenShots = timeLapseShots;
        currentNumBullets = numBullets;

        //index 1, weapon spit; index 2, split tongue
        currentWeaponImage.sprite = spritesWeapons[bulletIndex];
        UpdateNumberBullets();
        bulletInfo.SetActive(true);
    }

    public void UpdateNumberBullets()
    {
        textBullets.text = currentNumBullets.ToString();
    }


}
