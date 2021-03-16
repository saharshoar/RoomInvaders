using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damageAmount = 5;

    public float bulletSpeed = 5f;

    public Rigidbody theRB;

    private Vector3 direction;

    private PlayerController player = PlayerController.instance;

    // Start is called before the first frame update
    void Start()
    {
        direction = player.gameObject.transform.position - gameObject.transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.playerHealth.TakeDamage(damageAmount);
        }

        if (other.tag != "Enemy")
        {
            Destroy(gameObject, 0.001f);
        }
    }
}
