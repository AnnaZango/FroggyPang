using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPoints : MonoBehaviour
{
    //Controls player points

    [SerializeField] int points = 0;

    [SerializeField] TextMeshProUGUI textPoints;

    SoundManager soundManager;

    //sounds
    [SerializeField] AudioSource pickupSound;


    private void Awake()
    {    
        soundManager = FindObjectOfType<SoundManager>();        
    }

    void Start()
    {        
        UpdatePointsText();

        //destroy the singleton pattern of the music from first scenes
        if (soundManager != null)
        {
            Destroy(soundManager.gameObject);
        }
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
       
    public void PlayPickupSound()
    {
        pickupSound.Play();
    }


}
