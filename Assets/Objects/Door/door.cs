using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] Animator doorAnim;
    [SerializeField] bool doorEnabled = true;
    public GameObject areaToSpawn;
    private void Start()
    {
        // normally I would just add this in manually for a prefab but for some reason it doesn't seem to carry over when instantiated
        doorAnim = GetComponentInChildren<Animator>(); 
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || doorEnabled == true)
        {
            doorAnim.SetTrigger("OpenDoor");

            areaToSpawn.SetActive(true);
            doorEnabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {

    }
}
