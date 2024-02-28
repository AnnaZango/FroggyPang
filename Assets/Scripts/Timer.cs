using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // It controls the time the player has to finish each level.

    [SerializeField] int totalSeconds = 60;
    [SerializeField] TextMeshProUGUI timeText;

    int secondsLeft;
    
    public static Action OnTimeFinished; 

    
    void Start()
    {
        secondsLeft = totalSeconds;
        UpdateTextTime();

        //Instead of cheching the time on update, which would be called every frame, I invoke
        //the method every 1 second 
        InvokeRepeating(nameof(DecreaseSeconds), 1, 1);
    }

    private void DecreaseSeconds() 
    {
        if (GameManager.GetIfGameFinished()) { CancelInvoke(nameof(DecreaseSeconds)); }

        secondsLeft--;
        if (secondsLeft < 0)
        {
            secondsLeft = 0;
            OnTimeFinished?.Invoke(); //send the message that the time is up, listeners will react upon
            CancelInvoke(nameof(DecreaseSeconds));
        }
        UpdateTextTime();
    }

    private void UpdateTextTime()
    {
        timeText.text = secondsLeft.ToString();
    }

}
