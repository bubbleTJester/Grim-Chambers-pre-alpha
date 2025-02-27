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
    [SerializeField] private List<GameObject> spawns = new(); // worth saying the distribution breaks if the enemies aren't ordered from smallest chance to spawn to largest
    [SerializeField] Transform playerTransform; // double check later if this is still needed

    [Header("Spawn Rate Management")]
    [SerializeField] float spawnChance;
    [SerializeField] float distanceBetweenChecks;
    // [SerializeField] List<float> enemyChance = new();

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
                        Instantiate(EnemyCalculator(), hit.point, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform); 

                    }
                    
                }
            }
        }
    }

    private GameObject EnemyCalculator()
    {
        float numberGenerator = Random.Range(1, 100); // keeping this seperate to stop my head hurting from all these nested "for" and "if" statements
        for (int i = 0; i < spawns.Count; i++)
        {
            if(numberGenerator <= spawns[i].GetComponent<EnemyBehavior>().SpawnChance) // doing this every time is innefficient, set up an on start to do this only once
            {
                return spawns[i];
            }
        }
        return spawns[0]; // this is just exception handling, it'll spawn a grunt if this doesn't cover
    }
}
