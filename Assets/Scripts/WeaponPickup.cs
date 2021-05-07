using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject currentweapon;
    public GameObject weaponOnGround;
    // Start is called before the first frame update
    void Start()
    {
        currentweapon.SetActive(false);
        
    }
    void onTriggerEnter(Collider _collider)
    {
        if(_collider.gameObject.tag =="Player")
        {
            currentweapon.SetActive(true);
            weaponOnGround.SetActive(false);
        }
    }

    // Update is called once per frame
   
}
