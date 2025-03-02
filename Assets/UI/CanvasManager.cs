using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI armour;
    public TextMeshProUGUI ammo;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI timeAdd;

    public Image healthIndicator;
    public GameObject blood;

    public Sprite health1; // Full
    public Sprite health2; // Half
    public Sprite health3; // 25% or less

    public GameObject doorCheck;
    private static CanvasManager _instance;
    public static CanvasManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void UpdateHealth(int value)
    {
        health.text = value.ToString();
        UpdateHealthIndicator(value);

    }
    public void UpdateArmour(int value)
    {
        armour.text = value.ToString();
    }
    public void UpdateAmmo(int value)
    {
        if (value <= 0)
        {
            ammo.text = "MELEE";
        }
        else
        {
            ammo.text = value.ToString();
        }
        
    }
    public void UpdateHealthIndicator(int value) // makes that iconic doom face
    {
        if (value > 50)
        {
            healthIndicator.sprite = health1;
        }
        if (value <= 50 && value > 25)
        {
            healthIndicator.sprite = health2;
        }
        if (value <= 25)
        {
            healthIndicator.sprite = health3;
        }
    }
    public void DoorIndicator(bool passed) 
    {
        doorCheck.SetActive(passed);
    }

    internal void UpdateTime(float currentTime, bool timeAdded, float addition)
    {
        
        timer.text = (Math.Round(currentTime, 2)).ToString();

        if (timeAdded)
        {
            TextMeshProUGUI text = Instantiate(timeAdd, timeAdd.transform.position, transform.rotation);
            text.transform.SetParent(gameObject.transform, false);
            text.text = ("+" + addition.ToString() + "s");

        }
    }

    internal void Death()
    {
        blood.SetActive(true);
    }
}
