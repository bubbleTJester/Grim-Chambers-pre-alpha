using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float currentTime = FindAnyObjectByType<TimerManager>().currentTime;
            StatisticManager.LevelComplete(currentTime);
        }
        
    }
}
