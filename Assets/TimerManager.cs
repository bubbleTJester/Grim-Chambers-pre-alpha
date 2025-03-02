using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] public float currentTime;
    [SerializeField] float startTime;
    [SerializeField] PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = StatisticManager.TimeOnCompletion;
        if (currentTime == 0)
        {
            currentTime = startTime;
        }
        CanvasManager.Instance.UpdateTime(currentTime, false, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        if (currentTime <= 0)
        {
            EndPlayer();
            currentTime = 0; // just to avoid negatives showing on the UI
        }
        DisplayTime();
        if (Input.GetKeyDown(KeyCode.O))
        {
            AddTime(5f);
        }
    }
    private void DisplayTime()
    {
        CanvasManager.Instance.UpdateTime(currentTime, false, 0f);
    }
    private void EndPlayer()
    {
        playerHealth.KillPlayer();
    }

    public void AddTime(float addition)
    {
        currentTime += addition;
        CanvasManager.Instance.UpdateTime(currentTime, true, addition);

    }
}
