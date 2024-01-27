using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    //Controls player status in game (health, if it can be hurt, points...)

    [SerializeField] int points = 0;
    [SerializeField] int health = 1;
    [SerializeField] Shield shield;

    [SerializeField] TextMeshProUGUI textPoints;

    [SerializeField] bool canBeHurt = true;

    EndGameController endGameController;

    SoundManager soundManager;

    //sounds
    [SerializeField] AudioSource hurtSound;
    [SerializeField] AudioSource pickupSound;

    [SerializeField] Color32 colorDie;
    [SerializeField] SpriteRenderer[] spritesPlayer;

    private void OnEnable()
    {
        Timer.OnTimeFinished += Die;
    }

    private void OnDisable()
    {
        Timer.OnTimeFinished -= Die;
    }

    private void Awake()
    {
        //destroy the singleton pattern of the music from first scenes
        soundManager = FindObjectOfType<SoundManager>();
        spritesPlayer = GetComponentsInChildren<SpriteRenderer>(true); //true to get inactive sprites
        
        if(soundManager != null)
        {
            Destroy(soundManager.gameObject);
        }
    }

    void Start()
    {        
        UpdatePointsText();
        endGameController = FindObjectOfType<EndGameController>();
    }
           

    public void ChangePoints(int pointsToIncrease) 
    {
        //called everytime something gives points (destroying ball, point pickup)...
        if (!GameManager.GetPlayerAlive()) { return; }

        points += pointsToIncrease;
        UpdatePointsText();
    }

    private void UpdatePointsText()
    {
        textPoints.text = points.ToString();
    }

    public void GetHurt(int damage) 
    {
        //player has only 1 health point by default. If he has a shield on, balls can hit player 1 or
        //2 times without killing him, as impact ocurs on the shield.
        if (!GameManager.GetPlayerAlive()) { return; }

        if (canBeHurt)
        {
            health--;
            if (health <= 0)
            {
                Die();
            }
        }

    }

    private void ChangeColorSprites()
    {
        foreach (SpriteRenderer sprite in spritesPlayer)
        {
            sprite.color = colorDie;
        }
    }
   

    public void ActivateShield(int shieldHealth)
    {
        //The shield covers the player and prevents balls from colliding with him
        PlayPickupSound();
        shield.ActivateShield(shieldHealth);
    }

    public void PlayPickupSound()
    {
        pickupSound.Play();
    }

    
    private void Die()
    {
        GameManager.SetGameFinished(true);
        GameManager.SetPlayerAlive(false);
        GameManager.SetHasPlayerWon(false);
        ChangeColorSprites();
        hurtSound.Play();
        Time.timeScale = 0.2f; //reduce timescale on death. It is set back to 1 when end game panel
                               //appears
        endGameController.ShowPanelGameOver();
        Destroy(gameObject, 0.5f); //destroy player shortly after
    }

    public void SetCanBeHurt(bool hurt) //prevent player from being hurt for a certain time, like
                                        //when rotating scenario elements
    {
        if (GameManager.GetIfGameFinished()) { return; }
        canBeHurt = hurt;
        Invoke(nameof(ResetCanBeHurt), 3f);
    }

    private void ResetCanBeHurt()
    {
        if (GameManager.GetIfGameFinished()) { return; }
        canBeHurt = true;
    }

}
