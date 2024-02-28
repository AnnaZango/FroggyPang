using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndGameController : MonoBehaviour
{
    // This script controls when the panels of victory/game over appear at the end of the level. 

    [SerializeField] GameObject panelVictory;
    [SerializeField] GameObject panelLose;

    [SerializeField] AudioSource soundWin;
    [SerializeField] AudioSource soundLose;

    SceneController sceneController;

    public static Action OnGameFinished;


    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    void Start()
    {        
        panelLose.SetActive(false);
        panelVictory.SetActive(false);
    }

    private void OnEnable()
    {
        Timer.OnTimeFinished += ShowPanelGameOver;
    }

    private void OnDisable()
    {
        Timer.OnTimeFinished -= ShowPanelGameOver;
    }

   
    public void CheckNumberBalls() //called when the smallest balls are eliminated, to see if the
                                   //player has finished the level successfully
    {        
        if (GameManager.GetIfGameFinished()) { return; } //to prevent being called when time is up!
        
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball"); //check if ball of any kind left.
                                                                        //If so, game is still on
        if (balls.Length == 0)
        {
            WinGame();
        }
    }


    private void WinGame()
    {
        GameManager.SetGameFinished(true);
        GameManager.SetHasPlayerWon(true);
        StartCoroutine(ShowPanel());
    }

    public void ShowPanelGameOver()
    {
        if(GameManager.GetIfPlayerWins()) { return; }
        StartCoroutine(ShowPanel());
    }

    IEnumerator ShowPanel() //show victory or end panel depending on GetIfPlayerWins()
    {
        OnGameFinished?.Invoke();
        sceneController.SetDefaultSelectedButton();
        yield return new WaitForSeconds(0.6f);
        Time.timeScale = 1; //we set timescale back to normal
        if (GameManager.GetIfPlayerWins())
        {
            panelVictory.SetActive(true);
            soundWin.Play();
        }
        else
        {
            panelLose.SetActive(true);
            soundLose.Play();
        }
    }
}
