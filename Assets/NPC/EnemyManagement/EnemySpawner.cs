using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Make sure to add these before starting")]
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] Transform playerTransform;

    [Header("Spawn Rate Management")]
    [SerializeField] float spawnChance;
    [SerializeField] float distanceBetweenChecks;

    [Header("Raycasting stuff to keep them on stable ground")]
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
                if(Physics.Raycast(new Vector3(x, heightOfCheck, z), Vector3.down, out hit, rangeOfCheck, layerMask)) // checks stable ground
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
