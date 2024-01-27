using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneController : MonoBehaviour
{
    //It controls the movement from scene to scene.

    [SerializeField] int currentIndexScene;
    [SerializeField] bool splashScreen; //screens which must load the next one after a few seconds
    [SerializeField] float timeToLoadNext = 3f;
    [SerializeField] GameObject buttonNext;
    [SerializeField] GameObject buttonSame;

    int indexOffset = 3; //levels don't start at 0, as there is splash screen, main menu...Thus we
    //need an offset to not take into account those secenes when unlocking next level

    EventSystem eventSystem;

    
    void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        currentIndexScene = SceneManager.GetActiveScene().buildIndex;
        if (splashScreen)
        {
            //if it is a splash screen, move to next after the given time.
            Invoke(nameof(LoadNextScene), timeToLoadNext);
        }
    }

    public void SetDefaultSelectedButton()
    {
        //we set the default selected button when the panels appear at the end. It is important
        //to do this, as in gamepad there needs to be a default button selected

        if (GameManager.GetIfPlayerWins())
        {
            eventSystem.SetSelectedGameObject(buttonNext);
        }
        else
        {
            eventSystem.SetSelectedGameObject(buttonSame);
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentIndexScene + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadSameLevel() //for when failing to complete a level
    {
        SceneManager.LoadScene(currentIndexScene);
    }

    public void LoadSceneByIndex(int sceneIndex) //used when selecting a level
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void UnlockNextLevel()
    {
        //Called when a level is completed, to unlock the next level

        if(currentIndexScene>= PlayerPrefs.GetInt("numLevelsCompleted"))
        {
            PlayerPrefs.SetInt("numLevelsCompleted", (currentIndexScene + 1) - indexOffset);
        }
    }

    public void Quit()
    {
        Debug.Log("Quit game"); //for debugging purposes
        Application.Quit();
    }

   


}
