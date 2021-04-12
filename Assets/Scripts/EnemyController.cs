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
    public float shootRange = 8f;
    private float shootRangeAI = 7.5f;
    public float meleeRange = 1.5f;

    public Rigidbody theRB;
    public float moveSpeed = 4f;
    private float defaultSpeed;

    public int pointWorth = 100;

    public float playerDistance;

    public bool shouldShoot;
    public float fireRate = 1f;
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

    private bool newRound = true;
    private float newRoundCounter = 0.05f;

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

            if (SpawnManager.instance.enemyCount <= 0)
            {
                newRound = true;
            }
            if (newRound)
            {
                UpdateEnemyDifficulty();

                newRoundCounter -= Time.deltaTime;

                if (newRoundCounter <= 0)
                {
                    newRound = false;
                    newRoundCounter = 0.05f;
                }
            }
        }
    }

    private void UpdateEnemyDifficulty()
    {
        switch (SpawnManager.instance.roundNumber)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                moveSpeed = 1.5f;
                defaultSpeed = 5f;
                health = 1;
                damageDealt = 10;
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                moveSpeed = 2f;
                defaultSpeed = 10f;
                health = 3;
                damageDealt = 15;
                break;
            case 8:
            case 9:
            case 10:
                moveSpeed = 3.5f;
                defaultSpeed = 12f;
                health = 5;
                damageDealt = 15;
                break;
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
                moveSpeed = 6f;
                defaultSpeed = 15f;
                health = 6;
                damageDealt = 20;
                break;
            case 16:
            case 17:
            case 18:
            case 19:
                moveSpeed = 9f;
                defaultSpeed = 15f;
                health = 8;
                damageDealt = 20;
                break;
            case 20:
            default:
                moveSpeed = 12f;
                defaultSpeed = 20f;
                health = 10;
                damageDealt = 25;
                break;
                
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
            RangedAttack();

            if (playerDistance <= shootRangeAI)
            {
                goal = gameObject.transform;
            }
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
        health -= damage;
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
        if (PlayerController.instance.currentAmmo <= 20)
        {
            dropChance = Random.Range(30, 75);
        }
        else if (PlayerController.instance.playerHealth.currentHealth <= 25)
        {
            dropChance = Random.Range(60, 85);
        }

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
