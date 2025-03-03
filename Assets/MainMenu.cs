using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI text;
    public List<GameObject> buttonlist = new();
    public void StartGame()
    {
        StatisticManager.WipeAllStats(); // just incase
        SceneManager.LoadScene("Room builder");
        
    }
    public void Feedback()
    {
        if (StatisticManager.IsFeedback) // normally I would have this as a one and done but I know someone is gonna click this after not paying attention to instructions so best to have some error prevention
        {
            text.text = "Enable Feedback";
            StatisticManager.IsFeedback = false;
        }
        else
        {
            text.text = "Disable Feedback";
            StatisticManager.IsFeedback = true;
        }
    }
    public void Credits()
    {
        buttonlist[0].SetActive(false);
        buttonlist[1].SetActive(false);
        buttonlist[2].SetActive(false);
        buttonlist[3].SetActive(false);

        buttonlist[4].SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Back()
    {
        buttonlist[0].SetActive(true);
        buttonlist[1].SetActive(true);
        buttonlist[2].SetActive(true);
        buttonlist[3].SetActive(true);

        buttonlist[4].SetActive(false);
    }
    // there's probably a way to do this that involves less repeating so figure it out at somepoint
    public void LinkArk()
    {
        Application.OpenURL("https://bsky.app/profile/arkosol67.bsky.social");
    }
    public void LinkPati()
    {
        Application.OpenURL("https://www.youtube.com/@PatiIsSad/videos");
    }
    public void LinkBolt()
    {
        Application.OpenURL("https://www.youtube.com/@boltmarrow5887");
    }
    public void LinkMe()
    {
        Application.OpenURL("https://bsky.app/profile/bubbleteagames.itch.io");
    }
}
