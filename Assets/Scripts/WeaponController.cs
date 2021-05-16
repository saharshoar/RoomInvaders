using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public static WeaponController instance;
    public string WeaponName;
    public int damage = 1;
    public float firerate = 0.2f;
    public int currentAmmo = 50;
    public int maxAmmo = 150;
    public AudioSource gunsound, upgradedSound;
 
    public float aimShooting = 10;

    public bool available = false;


    public Image weaponImage;

    public Animator uiAnimator;
    bool _canShoot;
    int _currentAmmo;
    public void PlayGunsound()
    {
        gunsound.Stop();
        gunsound.Play();
    }
    public void PlayGunsoundUpgrade()
    {
        upgradedSound.Stop();
        upgradedSound.Play();
    }

    // Start is called before the first frame update
    void Start(){
        _currentAmmo = currentAmmo;
        _canShoot = true;

    }

    private void Update(){
        if (Input.GetMouseButton(0) && _currentAmmo > 0 )
        {
            _canShoot = false;
            _currentAmmo--;
            StartCoroutine(ShootGun());
        }
        IEnumerator ShootGun()
        {
            yield return new WaitForSeconds(firerate);
            _canShoot = true;
        }
    }

    // Update is called once per frame
    //    private void FireRateCountdown()
  //  {
    //    fireRateCounter -= Time.deltaTime;

      //  if (fireRateCounter <= 0)
        //{
         //   canFire = true;
          //  fireRateCounter = fireRate;
        //}
    //w}
}
