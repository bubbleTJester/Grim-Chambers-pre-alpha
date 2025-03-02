using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GetStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MonstersDefeated;
    [SerializeField] private TextMeshProUGUI ShotsFired;
    [SerializeField] private TextMeshProUGUI Accuracy;
    [SerializeField] private TextMeshProUGUI TotalDamage;
    [SerializeField] private TextMeshProUGUI LevelsCompleted;
    [SerializeField] private TextMeshProUGUI TimeLeft;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        MonstersDefeated.text = "Monsters Defeated: " + StatisticManager.Kills.ToString();
        ShotsFired.text = "Shots Fired: " + StatisticManager.ShotsFired.ToString();
        Accuracy.text = "Accuracy: " + StatisticManager.GetAccuracy().ToString();
        TotalDamage.text = "Total Damage Taken: " + StatisticManager.TotalDamageTaken.ToString();
        LevelsCompleted.text = "Floors Cleared: " + StatisticManager.LevelsCompleted.ToString();
        TimeLeft.text = "Time Remaining: " + StatisticManager.TimeOnCompletion.ToString();
    }
}
