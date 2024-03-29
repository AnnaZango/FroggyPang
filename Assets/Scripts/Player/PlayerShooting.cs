using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    //Controls player shooting mechanics and current bullet type

    [Header("Shooting variables")]
    [SerializeField] Transform shootingPosition;
    [SerializeField] GameObject defaultBullet;

    [Header("UI")]
    [SerializeField] GameObject bulletInfo; //text and image bullet, only active if weapon not default
    [SerializeField] TextMeshProUGUI textBullets;
    [SerializeField] Sprite[] spritesWeapons; //3 different types
    [SerializeField] Image currentWeaponImage; //0 is default

    [Header("Sounds")]
    [SerializeField] AudioSource shootSound;

    GameObject currentBullet;

    float defaultTimeBetweenShots = 1f;
    float timeBetweenShots = 1f; //modifiable depending on bullet type

    int currentNumBullets = 0;

    bool hasShot = false;


    void Start()
    {        
        currentBullet = defaultBullet;
        timeBetweenShots = defaultTimeBetweenShots;
        currentWeaponImage.sprite = spritesWeapons[0];
        bulletInfo.SetActive(false);
    }
        

    public void ShootAction(InputAction.CallbackContext context)
    {
        if (GameManager.GetIfGameFinished() || hasShot) { return; }

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
            SetToDefaultWeapon();
        }
    }

    private void SetToDefaultWeapon()
    {
        currentNumBullets = 0;
        currentBullet = defaultBullet;
        currentWeaponImage.sprite = spritesWeapons[0];
        timeBetweenShots = defaultTimeBetweenShots;
        bulletInfo.SetActive(false);
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

        //index 0, normal; index 1, weapon spit; index 2, split tongue
        currentWeaponImage.sprite = spritesWeapons[bulletIndex];
        UpdateNumberBullets();
        bulletInfo.SetActive(true);
    }

    public void UpdateNumberBullets()
    {
        textBullets.text = currentNumBullets.ToString();
    }


}
