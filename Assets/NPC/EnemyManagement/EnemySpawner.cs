using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] float spawnChance;

    [SerializeField] Transform playerTransform;
    [SerializeField] float distanceFromPlayer;
    [SerializeField] float distanceBetweenChecks;
    [SerializeField] float heightOfCheck = 10f, rangeOfCheck = 30f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector2 positivePosition, negativePosition;

    public void SpawnEnemy()
    {
        for(float x = negativePosition.x; x < positivePosition.x; x += distanceBetweenChecks)
        {
            for (float z = negativePosition.y; z < positivePosition.y; z += distanceBetweenChecks) // I know it says y, it's actually Z... gotta love Vector2
            {
                RaycastHit hit;
                if(Physics.Raycast(new Vector3(x, heightOfCheck, z), Vector3.down, out hit, rangeOfCheck, layerMask))
                {
                    if (Vector3.Distance(hit.point, playerTransform.position) > distanceFromPlayer) // I know I'd hate if I booted up the game and an enemy spawned right next to me
                    {
                        if (spawnChance > Random.Range(0, 101))
                        {
                            Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], hit.point, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
                        }
                    }
                }
            }
        }
    }
}
