using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private EnemyAwareness enemyAwareness;
    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if(enemyAwareness.isAggro)
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }
        else
        {
            navMeshAgent.SetDestination(transform.position);
        }
        
        
        
    }
}
