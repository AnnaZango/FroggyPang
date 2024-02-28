using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Controls the player health

    [SerializeField] Color32 colorDie;
    [SerializeField] AudioSource hurtSound;

    int health = 1;
    Shield shield;
    bool canBeHurt = true;

    EndGameController endGameController;

    SpriteRenderer[] spritesPlayer;
    PlayerPoints playerStats;

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
        spritesPlayer = GetComponentsInChildren<SpriteRenderer>(true); //true to get inactive sprites
        shield = GetComponentInChildren<Shield>(true);
        playerStats = GetComponent<PlayerPoints>();
        endGameController = FindObjectOfType<EndGameController>();
    }
          

    public void GetHurt(int damage)
    {
        //player has only 1 health point by default. If he has a shield on, balls can hit player 1 or
        //2 times without killing him, as impact ocurs on the shield.
        if (GameManager.GetIfGameFinished()) { return; }

        if (canBeHurt)
        {
            health--;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void ChangeColorSpritesToDie()
    {
        foreach (SpriteRenderer sprite in spritesPlayer)
        {
            sprite.color = colorDie;
        }
    }


    public void ActivateShield(int shieldPoints)
    {
        shield.GainShieldPoints(shieldPoints);
        playerStats.PlayPickupSound();
    }


    private void Die()
    {
        GameManager.SetGameFinished(true);
        GameManager.SetHasPlayerWon(false);
        ChangeColorSpritesToDie();
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
