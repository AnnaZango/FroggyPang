using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndGameController : MonoBehaviour
{
    // This script controls when the panels of victory/game over appear at the end of the level. 

    [SerializeField] GameObject panelVictory;
    [SerializeField] GameObject panelLose;

    SceneController sceneController;

    public static Action OnGameFinished;

    [SerializeField] AudioSource soundWin;
    [SerializeField] AudioSource soundLose;

    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
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
        StartCoroutine(CheckIfNoBallsLeft());
    }

    IEnumerator CheckIfNoBallsLeft() 
    {
        yield return new WaitForSeconds(0.1f); //slight delay
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball"); //check if ball of any kind left.
                                                                        //If so, game is still on

        if (balls.Length == 0)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        GameManager.SetHasPlayerWon(true);
        GameManager.SetGameFinished(true); 
        StartCoroutine(ShowPanel());
    }

    public void ShowPanelGameOver()
    {
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
