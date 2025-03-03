using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class StatisticManager
{
    public static int Kills { get; set; }
    public static int ShotsFired { get; set; }
    public static int LevelsCompleted { get; set; }
    public static int TotalDamageTaken { get; set; }

    public static float TimeOnCompletion {  get; set; }

    public static bool IsFeedback { get; set; }
    // all of these stat increases are in functions so that in future additional things can happen when these stats are incremented
    public static void WipeAllStats()
    {
        Kills = 0;
        ShotsFired = 0;
        LevelsCompleted = 0;
        TotalDamageTaken = 0;
        TimeOnCompletion = 60f;
    }
    public static void IncrementKills()
    {
        Kills++;
    }
    public static void ShotFired()
    {
        ShotsFired++;
    }
    public static float GetAccuracy()
    {
        return (float)Math.Round((float)Kills / ShotsFired, 2);
    }
    public static void LevelComplete(float currentTime)
    {
        LevelsCompleted++;
        TimeOnCompletion = currentTime;
        SceneManager.LoadScene("Room Builder");
    }
    public static void GameEnd(float currentTime)
    {
        TimeOnCompletion = currentTime;
        SceneManager.LoadScene("Game End");
    }

}
