using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyThirdController : MonoBehaviour
{
    public int health = 1;

    public int damageDealt = 10;

    public GameObject explosion;

    public float playerRange = 10f;
    //public float shootRange = 8f;
    private float RangeAI = 7.5f;
    public float meleeRange = 1.5f;


    public Rigidbody theRB;
    public float moveSpeed = 2f;
    private float defaultSpeed = 1f;


    public int pointWorth = 100;


    public float playerDistance;

   // public bool shouldShoot;
   // public float fireRate = 1f;
    //private float shotCounter = 2f;
    public float meleeCounter = 1f;
    private float meleeRate = 5f;
    //public GameObject bullet;
    //public Transform firePoint;


    private int dropChance;
    public GameObject ammoDrop;
    public GameObject healthDrop;
    public GameObject damageDrop;

    public bool hasAttacked;

    public Transform goal;
    NavMeshAgent agent;
    public bool inLineOfSight;


    private bool newRound = true;
    private float newRoundCounter = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        goal = PlayerController.instance.transform;

        //if (hasAttacked)
        //{
           // StartCoroutine(WaitPerSecond(5.00f));
            agent = GetComponent<NavMeshAgent>();
        //}

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
                moveSpeed = 2f;
                defaultSpeed = 3f;
                health = 1;
                damageDealt = 10;
                dropChance = Random.Range(1, 101);
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                moveSpeed = 3f;
                defaultSpeed = 5f;
                health = 2;
                damageDealt = 12;
                dropChance = Random.Range(1, 40);
                break;
            case 8:
            case 9:
            case 10:
                moveSpeed = 4.5f;
                defaultSpeed = 7f;
                health = 2;
                damageDealt = 15;
                dropChance = Random.Range(1, 20);
                break;
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
                moveSpeed = 6f;
                defaultSpeed = 8f;
                health = 3;
                damageDealt = 15;
                dropChance = Random.Range(60, 101);
                break;
            case 16:
            case 17:
            case 18:
            case 19:
                moveSpeed = 8f;
                defaultSpeed = 12f;
                health = 3;
                damageDealt = 20;
                dropChance = Random.Range(1, 101);
                break;
            case 20:
            default:
                moveSpeed = 10f;
                defaultSpeed = 15f;
                health = 4;
                damageDealt = 20;
                dropChance = Random.Range(1, 101);
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

        /* if (shouldShoot && playerDistance <= shootRange && inLineOfSight)
         {
             RangedAttack();

             if (playerDistance <= shootRangeAI)
             {

                 goal = gameObject.transform;
             }
         }

         else
         {*/
        // goal = PlayerController.instance.transform;

        // if (playerDistance < playerRange && !shouldShoot)
        // MeleeAttack();
        //}

        if (hasAttacked && playerDistance <= playerRange && inLineOfSight)
        {
            //wait 1.5 seconds
            StartCoroutine(WaitPerSecond(1.5f));

            if (playerDistance <= RangeAI)
            {
                goal = gameObject.transform;
            }
        }

        else
        {
            goal = PlayerController.instance.transform;

            if (playerDistance < playerRange && !hasAttacked)
                MeleeAttack();
        }
       
        
        
        
        
       /* if (playerDistance < playerRange)
        {
            MeleeAttack();
            hasAttacked = true;
        }

        else
        {
            //yield WaitForSeconds(5.0);
            StartCoroutine(WaitPerSecond(5.00f));
            hasAttacked = false;
        }*/

        agent.destination = goal.position;
    }

    IEnumerator WaitPerSecond(float duration)
    {
        //wait "waitTime"
        //agent = GetComponent<NavMeshAgent>();
        float startTime = Time.deltaTime;
        while (Time.time - startTime < duration)
            //agent = GetComponent<NavMeshAgent>();
        yield return null;

        //agent = GetComponent<NavMeshAgent>();

    }

    /*public void RangedAttack()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            shotCounter = fireRate;
        }
    }*/


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
        if (PlayerController.instance.currentAmmo <= 10)
        {
            dropChance = Random.Range(65, 75);
        }
        else if (PlayerController.instance.playerHealth.currentHealth <= 15)
        {
            dropChance = Random.Range(74, 81);
        }

        if (dropChance >= 70 && dropChance <= 74)
        {
           Instantiate(ammoDrop, transform.position, transform.rotation);
        }
        else if (dropChance >= 75 && dropChance <= 80)
        {
            Instantiate(healthDrop, transform.position, transform.rotation);
        }
        else if (dropChance >= 100)
        {
            Instantiate(ammoDrop, transform.position, transform.rotation);
            Instantiate(healthDrop, transform.position, transform.rotation);
        }
        else if (dropChance <= 10)
        {
            Instantiate(damageDrop, transform.position, transform.rotation);
        }
    }

}