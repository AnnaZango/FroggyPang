using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager 
{
    // This script controls general states of the game, such as if the player is alive or if game has
    // ended

    private static bool IsGameFinished;
    private static bool playerWins;

    public static void SetGameFinished(bool isGameOver)
    {
        IsGameFinished = isGameOver;
    }
    public static bool GetIfGameFinished()
    {
        return IsGameFinished;
    }

    public static void SetHasPlayerWon(bool hasWon)
    {
        playerWins = hasWon;
    }
    public static bool GetIfPlayerWins()
    {
        return playerWins;
    }

}
