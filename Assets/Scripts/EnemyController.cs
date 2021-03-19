using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 3;
    public GameObject explosion;

    public float playerRange = 10f;
    public float shootRange = 5f;

    public Rigidbody theRB;
    public float moveSpeed = 2.5f;

    public int pointWorth = 100;

    public bool shouldShoot;
    public float fireRate = 0.5f;
    private float shotCounter = 5f;
    public GameObject bullet;
    public Transform firePoint;

    private int dropChance;
    public GameObject ammoDrop;
    public GameObject healthDrop;

    // Start is called before the first frame update
    void Start()
    {
        dropChance = Random.Range(1, 101);
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        if (shouldShoot)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange
                && Vector3.Distance(transform.position, PlayerController.instance.transform.position) > shootRange)
            {
                Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;

                theRB.velocity = playerDirection.normalized * moveSpeed;

            }
            else if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= shootRange && shouldShoot)
            {
                theRB.velocity = Vector3.zero;

                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    shotCounter = fireRate;
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange)
            {
                Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;

                theRB.velocity = playerDirection.normalized * moveSpeed;

            }
            else
            {
                theRB.velocity = Vector3.zero;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        if (health <= 0)
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

            RewardPoints(pointWorth);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject, 0.001f);
        }
    }

    public void RewardPoints(int points)
    {
        PlayerController.instance.currentPoints += points;
        PlayerController.instance.totalPoints += points;
    }
}
