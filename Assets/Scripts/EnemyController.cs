using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int health = 3;
    public int damageDealt = 20;
    public GameObject explosion;

    public float playerRange = 10f;
    public float shootRange = 5f;
    public float meleeRange = 2f;

    public Rigidbody theRB;
    public float moveSpeed = 4f;
    private float defaultSpeed;

    public int pointWorth = 100;

    public float playerDistance;

    public bool shouldShoot;
    public float fireRate = 0.5f;
    private float shotCounter = 2f;
    public float meleeCounter = 1f;
    private float meleeRate = 1f;
    public GameObject bullet;
    public Transform firePoint;

    private int dropChance;
    public GameObject ammoDrop;
    public GameObject healthDrop;
    public GameObject damageDrop;

    public Transform goal;
    NavMeshAgent agent;
    public bool inLineOfSight;

    // Start is called before the first frame update
    void Start()
    {
        dropChance = Random.Range(1, 101);

        goal = PlayerController.instance.transform;

        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;

        defaultSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.instance.hasDied)
        {
            MoveEnemy();

            if (SpawnManager.instance.roundNumber == 5)
            {
                moveSpeed = 5f;
                defaultSpeed = 4f;
            }
            else if (SpawnManager.instance.roundNumber == 10)
            {
                moveSpeed = 6.5f;
                defaultSpeed = 5f;
            }
            else if (SpawnManager.instance.roundNumber == 25)
            {
                moveSpeed = 9f;
                defaultSpeed = 7f;
            }
        }
    }
            
    public void MoveEnemy()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, (PlayerController.instance.transform.position - transform.position), out hit))
        {
            if (hit.transform.tag == "Player")
            {
                inLineOfSight = true;
                agent.speed = moveSpeed;
            }
            else
            {
                inLineOfSight = false;
                agent.speed = defaultSpeed;
            }
        }

        playerDistance = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        if (shouldShoot && playerDistance <= shootRange && inLineOfSight)
        {
            goal = gameObject.transform;
            RangedAttack();
        }
        else
        {
            goal = PlayerController.instance.transform;

            if (playerDistance < playerRange && !shouldShoot)
                MeleeAttack();
        }

        agent.destination = goal.position;
    }

    public void RangedAttack()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            shotCounter = fireRate;
        }
    }

    public void MeleeAttack()
    {
        meleeCounter -= Time.deltaTime;

        if (playerDistance <= meleeRange && meleeCounter <= 0)
        {
            PlayerController.instance.playerHealth.TakeDamage(damageDealt);
            meleeCounter = meleeRate;
        }
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        AudioController.instance.PlayEnemyShot();

        if (health <= 0)
        {
            DropItem();

            PlayerController.instance.RewardPoints(pointWorth);
            AudioController.instance.PlayEnemyDeath();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject, 0.001f);
        }
    }

    private void DropItem()
    {
        if (dropChance >= 50 && dropChance <= 74)
        {
            Instantiate(ammoDrop, transform.position, transform.rotation);
        }
        else if (dropChance >= 75 && dropChance <= 99)
        {
            Instantiate(healthDrop, transform.position, transform.rotation);
        }
        else if (dropChance >= 100)
        {
            Instantiate(ammoDrop, transform.position, transform.rotation);
            Instantiate(healthDrop, transform.position, transform.rotation);
        }
        else if (dropChance <= 20)
        {
            Instantiate(damageDrop, transform.position, transform.rotation);
        }
    }
}
