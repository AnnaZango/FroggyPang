using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // It is attached to the player's shield (a child from the player) and it controls its
    // functionality. 

    [SerializeField] int currentHealth = 1; //it can be 1 or 2 depending on shield type (normal or super)
    [SerializeField] Color32 colorSuperShield; //dark color, more protection
    [SerializeField] Color32 colorNormalShield; //light color, less protective
    SpriteRenderer spriteRenderer;
        
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

   
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag== "ball")
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                //if it still has health, it becomes a normal shield (not super anymore)
                spriteRenderer.color = colorNormalShield;
            }
        }
    }

    public void ActivateShield(int shieldHealth) //it gets the health (1 or 2) from pickup
    {
        //called when colliding with shield pickup, it activates the shield Game Object.
        if(shieldHealth < currentHealth) { return; } 
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = shieldHealth;
        if (shieldHealth > 1)
        {
            spriteRenderer.color = colorSuperShield;
        }
        else
        {
            spriteRenderer.color = colorNormalShield;
        }
        gameObject.SetActive(true);
    }
}
