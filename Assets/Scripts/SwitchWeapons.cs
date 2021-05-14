using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SwitchWeapons : MonoBehaviour
{
    public static SwitchWeapons instance;
    public GameObject[] weapons;
    int weaponCounter = 0;
    float fireRate = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
            weapons[0].SetActive(true);
        }
        weapons[0].GetComponent<WeaponController>().available = true;
        weapons[1].GetComponent<WeaponController>().available = true;
        //weapons[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        int newWeapon = weaponCounter;


        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
                newWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
                newWeapon--;
        }

       
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            newWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            newWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            newWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            newWeapon = 3;
        }
        
        if (newWeapon != weaponCounter)
        {
            Debug.Log("Switching to weapon index " + newWeapon.ToString());
            SwitchToWeapon(newWeapon);
        }
        
            
    }

    public void SwitchToWeapon(string weaponName)
    {
        Debug.Log("Searching for weapon " + weaponName);
        int newWeapon = weaponCounter;

        for(int i = 0; i < weapons.Length; i++)
        {
            var w = weapons[i].GetComponent<WeaponController>();
            //Debug.Log("Looking at weapon index " + i.ToString() + ", name " + weapons[i].name);
            if (w.WeaponName == weaponName)
            {
                w.available = true;
                Debug.Log("Found weapon " + weaponName + " at index " + i.ToString());
                newWeapon = i;
            }
        }

        SwitchToWeapon(newWeapon);
    }

    public void SwitchToWeapon(int newWeapon)
    {
        
        
        if (newWeapon == weaponCounter) return;

        if (newWeapon < 0 || newWeapon >= weapons.Length) return;

        if (weapons[newWeapon].GetComponent<WeaponController>().available == false) return;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        weaponCounter = newWeapon;

        weapons[weaponCounter].SetActive(true);

        var w = weapons[weaponCounter].GetComponent<WeaponController>();
        PlayerController.instance.currentWeapon = w;
        PlayerController.instance.fireRateCounter = w.firerate;
    }
}
