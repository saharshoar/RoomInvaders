﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    // Change this variable in the inspector to change depending on type of pickup
    public int pickupAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the pick up is an ammo pickup, and the player collides with it
        if (other.tag == "Player" && gameObject.tag == "ammo pickup")
        {
            PlayerController.instance.currentAmmo += pickupAmount;

            Destroy(gameObject, 0.01f);
        }

        if (other.tag == "Player" && gameObject.tag == "health pickup")
        {
            PlayerController.instance.playerHealth.AddHealth(pickupAmount);

            Destroy(gameObject, 0.01f);
        }
    }
}