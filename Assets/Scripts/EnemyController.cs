using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 3;
    public GameObject explosion;

    public float playerRange = 10f;

    public Rigidbody theRB;
    public float moveSpeed = 2.5f;

    public int pointWorth = 100;

    public bool shouldShoot;
    public float fireRate = 0.5f;
    private float shotCounter = 5f;
    public GameObject bullet;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange)
        {
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;

            theRB.velocity = playerDirection.normalized * moveSpeed;

            if(shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter <= 0)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    shotCounter = fireRate;
                }
            }
        }
        else
        {
            theRB.velocity = Vector3.zero;
        }
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            RewardPoints(pointWorth);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void RewardPoints(int points)
    {
        PlayerController.instance.currentPoints += points;
        PlayerController.instance.totalPoints += points;
    }
}
