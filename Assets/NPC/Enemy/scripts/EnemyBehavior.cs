using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] List<GameObject> dropTable = new();
    [SerializeField] SpriteChanger spriteChanger;
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> noises = new(); // 0 = hurt, 1 = dead, 2 = attacking, 3 = alert
    private bool avoidAudioLockout = false; // stops alert sound from overwriting the rest

    public float SpawnChance; // there's probably a better solution to choosing the chance for each enemy to spawn like having a class system, but this will do
    public NavMeshAgent agent;
    public GameObject player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;


    //patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public int timeout = 1000;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public int damage;
    // public GameObject projectile;
    public Transform shotPoint;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //when dead
    [SerializeField] GameObject gunHitEffect;

    // enemy manager
    public EnemyManager manager;

    [Header("CLICK TO TURN OFF ENEMY MOVEMENT")]
    public bool movement;

    [Header("Makes enemy fire a projectile instead of using a raycast")]
    [SerializeField] private bool isRanged;
    [SerializeField] private GameObject projectile;

    // avoiding spawns within same room as player
    [Header("Start Room Spawn Prevention")]
    [SerializeField] float distanceFromPlayer;
    private bool dead = false;
    private TimerManager timerManager;
    private void Awake()
    {
        timerManager = FindAnyObjectByType<TimerManager>();
        
        agent = GetComponent<NavMeshAgent>();

        manager = GetComponentInParent<EnemyManager>();
        
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (Physics.CheckSphere(transform.position, distanceFromPlayer, whatIsPlayer)) // KILL SPAWNKILLERS
        {
            // Debug.Log("Enemy In Range");

            // If you haven't already, you're probably going to see a lot of raycasts that look like this, CAUSE IF IT AINT BROKE...
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit, distanceFromPlayer, whatIsPlayer))
            {
                // Debug.Log("TAKE THE SHOT");
                Destroy(gameObject);
            }
        }
    }


    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); // check for player inside view distance of unit
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); // check for player inside attack distance of unit
        if (!dead)
        {
            if (movement)
            {
                if (!playerInSightRange && !playerInAttackRange) Patrolling(); // set unit to patrol area

                if (playerInSightRange && !playerInAttackRange) { ChasePlayer(); avoidAudioLockout = true; } // set unit to pathfind to player until within attack range
                else
                {
                    avoidAudioLockout = false;
                }
            }
            if (playerInAttackRange && playerInSightRange) AttackPlayer(); // set unit to fire at player

            timeout -= 1; // reset patrol if stuck
        }
        else
        {
            agent.SetDestination(transform.position);
        }
        

    }

    private void SearchWalkPoint()
    {
        // Return random values based on value of walkPointRange, effectively making a sphere around the unit in which it will randomly choose a point to reach
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) // checks walkPoint is safe ground that the unit can stand on. if it passes, the unit can move to the point, otherwise it checks for a new position.
        {
            walkPointSet = true;
        }
    }



    private void Patrolling()
    {
            spriteChanger.AnimationState = 0; 
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);


            }

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            // Walk Point Reached
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
            if (timeout <= 0) // if the timeout reaches 0, find a new point and try again
            {
                SearchWalkPoint();
                timeout = 1000;
            }
    }

    private void ChasePlayer()
    {
        spriteChanger.AnimationState = 0;
        if (!avoidAudioLockout)
        {
            PlayAudio(3);
        }
        agent.SetDestination(player.transform.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position); // ensures the enemy does not continue to follow the player when within range

        transform.LookAt(player.transform);

        if (!alreadyAttacked)
        {
            PlayAudio(2);
            Debug.Log("attacking");
            //Damage player | Should probably do some more checks but this will do until I find a problem with it
            if (isRanged)
            {
                RangedAttack(); 
            }
            else
            {
                MeleeAttack();
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        
    }
    private void MeleeAttack()
    {
        if (Physics.Raycast(shotPoint.position, player.transform.position - shotPoint.position, out RaycastHit hit, distanceFromPlayer, whatIsPlayer))
        {
            Debug.Log("Successful Attack");
            spriteChanger.AnimationState = 1;
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.DamagePlayer(damage);
        }
    }
    private void RangedAttack()
    {
        Rigidbody rb = Instantiate(projectile, shotPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 3f, ForceMode.Impulse);
        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
        spriteChanger.AnimationState = 0;
    }


    private void PlayAudio(int noise)
    {
        audioSource.clip = noises[noise];
        audioSource.pitch = Random.Range(0.75f, 1.25f);
        audioSource.Play();
    }



    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            PlayAudio(0);
            Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        }
        
        health -= damage;
        
        if (health <= 0) // put enemy death stuff here
        {
            dead = true;
            if (Random.Range(1, 6) >= 4)
            {
                Instantiate(dropTable[Random.Range(0, dropTable.Count)], transform.position, Quaternion.identity);
            }
            float addedTime = (float)(Random.Range(3, 5));
            timerManager.AddTime(addedTime);
            PlayAudio(1);
            manager.RemoveEnemy(this);
            Destroy(gameObject, noises[1].length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}
