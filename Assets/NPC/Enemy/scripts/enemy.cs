using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public EnemyManager manager;
    private float enemyHealth = 2f;

    [SerializeField] GameObject gunHitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            manager.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        enemyHealth -= damage;

        
    }
}
