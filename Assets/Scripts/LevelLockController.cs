using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLockController : MonoBehaviour
{
    // It uses PlayerPrefs to unlock levels as they are completed.

    int numLevelsCompleted = 1; //we start with first level unlocked
    [SerializeField] Button[] buttonsLevels;

    
    void Start()
    {
        //PlayerPrefs.DeleteAll(); // use this to reset PlayerPrefs and just have 1 level unlocked!
        numLevelsCompleted = PlayerPrefs.GetInt(nameof(numLevelsCompleted), 1);
        for (int i = 0; i < buttonsLevels.Length; i++)
        {
            buttonsLevels[i].interactable = false; //non interactable first
        }

        for (int i = 0; i < numLevelsCompleted; i++)
        {
            buttonsLevels[i].interactable = true; //unlock buttons of unlocked levels
        }
    }
    
}
