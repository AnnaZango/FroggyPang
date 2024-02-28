using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLockController : MonoBehaviour
{
    // It uses PlayerPrefs to unlock levels as they are completed.

    [SerializeField] Button[] buttonsLevels;
    int numLevelsCompleted = 1; //we start with first level unlocked
    
    void Start()
    {
        EnableButtonsBasedOnLevelsBeaten();
    }

    private void EnableButtonsBasedOnLevelsBeaten()
    {
        //we need to add a default value or it will be 0, and then no levels will be available
        numLevelsCompleted = PlayerPrefs.GetInt("numLevelsCompleted", 1);
        for (int i = 0; i < buttonsLevels.Length; i++)
        {
            buttonsLevels[i].interactable = false; //non interactable first
        }

        for (int i = 0; i < numLevelsCompleted; i++)
        {
            buttonsLevels[i].interactable = true; //unlock buttons of unlocked levels
        }
    }

    public void ResetBeatedLevels()
    {
        PlayerPrefs.DeleteAll();
        EnableButtonsBasedOnLevelsBeaten();
    }
    
}
