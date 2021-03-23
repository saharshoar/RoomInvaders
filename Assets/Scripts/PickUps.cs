using System.Collections;
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

            if (PlayerController.instance.currentAmmo >= PlayerController.instance.maxAmmo)
            {
                PlayerController.instance.currentAmmo = PlayerController.instance.maxAmmo;
            }

            AudioController.instance.PlayAmmoPickup();

            Destroy(gameObject, 0.01f);
        }

        if (other.tag == "Player" && gameObject.tag == "health pickup")
        {
            PlayerController.instance.playerHealth.AddHealth(pickupAmount);
            AudioController.instance.PlayHealthPickup();

            Destroy(gameObject, 0.01f);
        }

        if (other.tag == "Player" && gameObject.tag == "damage pickup")
        {
            PlayerController.instance.damagePickup = true;
            AudioController.instance.PlayAmmoPickup();

            Destroy(gameObject, 0.01f);
        }
    }
}
