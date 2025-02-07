using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] Animator doorAnim;
    public GameObject areaToSpawn;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("OpenDoor");

            areaToSpawn.SetActive(true);
        }
    }
}
