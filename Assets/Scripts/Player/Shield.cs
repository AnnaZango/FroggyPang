using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // It is attached to the player's shield (a child from the player) and it controls its
    // functionality. 

    [Range(0,2)] [SerializeField] int currentPoints = 0; //it can be 0 (no shield), 1, or 2 depending on shield type (normal or super)
    [SerializeField] Color32 colorSuperShield; //dark color, more protection
    [SerializeField] Color32 colorNormalShield; //light color, less protective
    SpriteRenderer spriteRenderer;
    Collider2D shieldCollider;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(true);
        shieldCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        SetShieldBasedOnPoints();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag== "ball")
        {
            currentPoints--;
            SetShieldBasedOnPoints();
        }
    }

    private void SetShieldBasedOnPoints()
    {
        if(currentPoints <= 0)
        {
            currentPoints = 0;
            transform.GetChild(0).gameObject.SetActive(false);
            shieldCollider.enabled = false;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            shieldCollider.enabled = true;

            switch(currentPoints)
            {
                case 1:                   
                    spriteRenderer.color = colorNormalShield;
                    break;
                case 2:                    
                    spriteRenderer.color = colorSuperShield;
                    break;
            }
        }
    }

    public int GetCurrentShieldPoints()
    {
        return currentPoints;
    }

    public void GainShieldPoints(int shieldPointsToAdd) //it gets the health (1 or 2) from pickup
    {
        int totalPoints = Mathf.Clamp((currentPoints + shieldPointsToAdd), 0, 2);
        currentPoints = totalPoints;
        SetShieldBasedOnPoints();
    }
}
