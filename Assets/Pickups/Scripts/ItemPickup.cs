using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    [SerializeField] bool isHealth;
    [SerializeField] bool isArmour;
    [SerializeField] bool isAmmo;

    [SerializeField] int amount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player enter");
            if (isHealth)
            {
                Debug.Log("Health");
                other.GetComponent<PlayerHealth>().GiveHealth(amount, this.gameObject);
            }
            if (isArmour)
            {
                Debug.Log("Armour");
                other.GetComponent<PlayerHealth>().GiveArmour(amount, this.gameObject);
            }
            if (isAmmo)
            {
                Debug.Log("Ammo");
                other.GetComponentInChildren<Gun>().GiveAmmo(amount, this.gameObject);
            }
            
        }
    }
}
