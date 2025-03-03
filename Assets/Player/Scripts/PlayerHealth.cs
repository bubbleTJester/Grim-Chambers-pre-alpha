using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TimerManager timerManager;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] Animator animator;

    [Header("Set Audio")]
    [SerializeField] List<AudioClip> audioClips = new(); // 0 = health, 1 = armour
    [SerializeField] AudioSource audioSource;


    [Header("Set Health And Armour")]
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
        
        if (isDead)
        {
            KillPlayer();
            StartCoroutine(Coroutine());
            
            
        }   
    }
    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(3f);
        StatisticManager.GameEnd(timerManager.currentTime);
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

        if (health <= 0)
        {
            isDead = true;
        }
        if (health < 0)
        {
            health = 0;
        }
        CanvasManager.Instance.UpdateHealth(health);
        CanvasManager.Instance.UpdateArmour(armour);

        if (StatisticManager.IsFeedback)
        {
            CanvasManager.Instance.Hurt();
        }
        
        StatisticManager.TotalDamageTaken += damage;
    }
    private void PlayAudio(int noise)
    {
        if (StatisticManager.IsFeedback) // REMOVE AFTER STUDY
        {
            audioSource.clip = audioClips[noise];
            audioSource.pitch = Random.Range(0.75f, 1.25f);
            audioSource.Play();
        }

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
        PlayAudio(0);
        
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
        PlayAudio(1);
    }
    private void OnTriggerEnter(Collider collision) // a bit jank but it will do for now
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            DamagePlayer(5);
            Destroy(collision.gameObject);
        }
    }
    public void KillPlayer() // Dying Goes Here
    {
        animator.SetBool("isDead", true);
        mouseLook.enabled = false;
        playerMovement.enabled = false;
        CanvasManager.Instance.Death();
    }
}
