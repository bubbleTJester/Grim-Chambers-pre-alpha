using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    private int health;

    [SerializeField] int maxArmour;
    private int armour;

    public bool isDead = false;
    private void Start()
    {
        health = maxHealth;

        CanvasManager.Instance.UpdateHealth(health);
        CanvasManager.Instance.UpdateArmour(armour);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // DEBUG || REMOVE LATER
        {
            DamagePlayer(20);
        }
        
            

        

        
    }
    public void DamagePlayer(int damage)
    {
        if (armour > 0)
        {
            if (armour >= damage) // armour can absorb all damage
            {
                armour -= damage;
            }
            else if (armour < damage) // armour can absorb partial damage
            {
                int remainingDamage;
                remainingDamage = damage - armour;

                armour = 0;
                health -= remainingDamage;
            }
            
        }
        else
        {
            health -= damage;
        }

        if (health <= 0) // add death logic later
        {
            isDead = true;
        }
        CanvasManager.Instance.UpdateHealth(health);
        CanvasManager.Instance.UpdateArmour(armour);

    }
    public void GiveHealth(int amount, GameObject pickup)
    {
        
        if (health < maxHealth)
        {
            health += amount;
            Destroy(pickup);
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        CanvasManager.Instance.UpdateHealth(health);
    }
    public void GiveArmour(int amount, GameObject pickup)
    {
        
        if (armour < maxArmour)
        {
            armour += amount;
            Destroy(pickup);
        }
        
        if (armour > maxArmour)
        {
            health = maxArmour;
        }
        CanvasManager.Instance.UpdateArmour(armour);
    }
}
