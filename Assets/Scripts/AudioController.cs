using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioSource ammo, enemyDeath, enemyShot, gunshot, health, playerHurt, gunshotUpgrade, footstepOutside, footstepInside, openDoor, cantopenDoor, pistolShot, rifleShot, miniShot;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayopenDoor()
    {
        openDoor.Play();
    }

    public void PlaycantopenDoor()
    {
        cantopenDoor.Stop();
        cantopenDoor.Play();
    }

    public void PlayfootstepOutside()
    {
        //footstepOutside.Stop();
        footstepOutside.Play();
    }
    public void PlayfootstepInside()
    {
        //footstepInside.Stop();
        footstepInside.Play();
    }

    public void PlayAmmoPickup()
    {
        ammo.Stop();
        ammo.Play();
    }
    public void PlayEnemyDeath()
    {
        enemyDeath.Stop();
        enemyDeath.Play();
    }
    public void PlayEnemyShot()
    {
        enemyShot.Stop();
        enemyShot.Play();
    }
    public void PlayGunshot()
    {
        gunshot.Stop();
        gunshot.Play();
    }
    public void PlaypistolShot()
    {
        pistolShot.Stop();
        pistolShot.Play();
    }
    public void PlayrifleShot()
    {
        rifleShot.Stop();
        rifleShot.Play();
    }
    public void PlayminiShot()
    {
        miniShot.Stop();
        miniShot.Play();
    }
    public void IncreaseGunshot()
    {
        gunshot = gunshotUpgrade;
        gunshot.volume = 0.6f;
    }

    public void PlayHealthPickup()
    {
        health.Stop();
        health.Play();
    }
    public void PlayPlayerHurt()
    {
        playerHurt.Stop();
        playerHurt.Play();
    }
    public void PlayGunshotUpgrade()
    {
        gunshotUpgrade.Stop();
        gunshot.Play();
    }
}
