using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject ammoPickup;


    [Header("Gun Stats")]
    [SerializeField] float range;
    [SerializeField] float verticalRange;
    [SerializeField] float meleeRange;
    [SerializeField] float gunDamage;
    [SerializeField] float fireRate;

    // [SerializeField] float gunShotRadius;
    [SerializeField] LayerMask enemyLayerMask;

    private float nextTimeToFire;
    private BoxCollider gunTrigger;

    private int ammo;
    [SerializeField] int maxAmmo;


    [SerializeField] LayerMask raycastLayerMask;
    [SerializeField] EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo / 2;

        gunTrigger = GetComponent<BoxCollider>();
       
        CanvasManager.Instance.UpdateAmmo(ammo);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire)
        {
            Fire();
        }
    }
    void Fire() // the goal was to create a doom-like gun that ignores altitude while still taking walls into consideration
    {
        // Gun Audio
        
        if(ammo <= 0)
        {
            gunTrigger.size = new Vector3(2, verticalRange, meleeRange);
            gunTrigger.center = new Vector3(0, 0, meleeRange / 2);
        }
        else
        {
            GetComponent<AudioSource>().pitch = Random.Range(0.75f, 1.25f);
            GetComponent<AudioSource>().Play();

            gunTrigger.size = new Vector3(1, verticalRange, range);
            gunTrigger.center = new Vector3(0, 0, range / 2);
            // ammo counter
            ammo -= 2;
            CanvasManager.Instance.UpdateAmmo(ammo);

            // reset Timer
            nextTimeToFire = Time.time + fireRate;
        }

        // damage enemies
        // This is to avoid returning null when firing at thin air. Don't know if It'd break anything, but I don't plan on finding out either
        if (enemyManager.enemiesInTrigger.Count > 0) 
        {
            DamageEnemies();
        }


        // simulate gunshot radius | revisit another time
        //Collider[] enemyColliders;
        //enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);
        //foreach (var enemyCollider in enemyColliders)
        //{
        //    enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
        //}

    }

    private void DamageEnemies()
    {
        foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            // get direction to enemy
            var dir = enemy.transform.position - transform.position;


            RaycastHit hit;

            if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))
            {
                if (hit.transform == enemy.transform)
                {
                    // range check || change later to be more dynamic e.g. after a set range reduce the damage over distance rather than just half straight away
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if (dist > range / 2) { enemy.TakeDamage(gunDamage / 2); }
                    else { enemy.TakeDamage(gunDamage); }

                    if (ammo <= 0)
                    {
                        Instantiate(ammoPickup, enemy.transform.position, Quaternion.identity);
                    }
                }
            }

        }
    }

    public void GiveAmmo(int amount, GameObject pickup)
    {
        if(ammo < maxAmmo)
        {
            ammo += amount;
            Destroy(pickup);
        }

        if (ammo > maxAmmo)
        {
            ammo = maxAmmo;
        }
        CanvasManager.Instance.UpdateAmmo(ammo);
    }
    private void OnTriggerEnter(Collider other)
    {
        // add potential enemy
        EnemyBehavior enemy = other.transform.GetComponent<EnemyBehavior>();
        if (enemy)
        {
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // remove potential enemy
        EnemyBehavior enemy = other.transform.GetComponent<EnemyBehavior>();
        if (enemy)
        {
            enemyManager.RemoveEnemy(enemy);
        }
    }
}
