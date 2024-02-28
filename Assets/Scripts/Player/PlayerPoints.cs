using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPoints : MonoBehaviour
{
    //Controls player points

    [SerializeField] TextMeshProUGUI textPoints;
    [SerializeField] AudioSource pickupSound;

    private int points = 0;
    SoundManager soundManager;

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
        if (GameManager.GetIfGameFinished()) { return; }

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
