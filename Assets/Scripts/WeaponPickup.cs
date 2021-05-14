using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string weaponPickupName = "rifle";
    public string weaponMgun = "mgun";


    //public GameObject currentweapon;
    //public GameObject weaponOnGround;
    // Start is called before the first frame update
    void Start()
    {
        //currentweapon.SetActive(false);
        
    }

    void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Debug.Log("Collided with " + name + ", tag " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player") 
        {
            SwitchWeapons.instance.SwitchToWeapon(weaponPickupName);
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
   
}
