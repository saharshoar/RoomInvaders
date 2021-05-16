using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // To do: Rearrange variables based on use

    public static PlayerController instance;
    public PlayerHealth playerHealth;

    private Rigidbody playerRb;

    public float moveSpeed = 6f;
    public float mouseSensitivity = 4f;

    private Vector3 moveInput;
    private Vector2 mouseInput;

    public Camera playerCam;

    public GameObject bulletImpact;
    public Animator shotGunUI;
    public Animator rifleUI;
    public Animator pistolUI;
    public Animator anim;

    public int currentAmmo = 0;
    public int maxAmmo = 150;

    public int damageDealt = 1;
    public int tempDamageDealt = 1;
    public bool damagePickup = false;
    public bool currentlyPoweredUp = false;
    public float powerUpCounter = 30f;

    public int currentPoints = 0;
    public int totalPoints = 0;

    public GameObject deadScreen;
    public GameObject pointsBox;
    public GameObject perkBox;
    public GameObject ammoBox;
    public GameObject roundBox;
    public bool hasDied = false;
    public bool canMove = true;

    public Text totalPointsText;
    public Text ammoText;
    public Text pointsText;
    public Text roundText;

    public int playerZone = 1;

    public int[] perks;
    public int perkArrayLoc = 0;
    public bool hasHealthPerk = false;
    public bool hasSpeedPerk = false;
    public bool hasDamagePerk = false;
    public int healthPerkVal = 0;
    public int speedPerkVal = 1;
    public int damagePerkVal = 2;
    public GameObject perkSlot1;
    public GameObject perkSlot2;
    public GameObject perkSlot3;
    public Sprite[] perkSprites;

    public Image shotGunImage;

    public WeaponController[] weapons;

    public WeaponController currentWeapon = null;
    public float fireRate = 0.3f;
    public float fireRateCounter = 0.3f;
    private bool canFire = false;

    public bool hasFlashlight = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerRb = GetComponent<Rigidbody>();
        ammoText.text = currentWeapon.currentAmmo.ToString();
        pointsText.text = currentPoints.ToString();
        pointsBox.SetActive(false);
        perkBox.SetActive(false);
        perkSlot1.SetActive(false);
        perkSlot2.SetActive(false);
        perkSlot3.SetActive(false);

        roundBox.SetActive(true);
        ammoBox.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDied && canMove)
        {
            // Player movement
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            Vector3 moveHorizontal = transform.right * moveInput.x;
            Vector3 moveVertical = transform.forward * moveInput.z;

            playerRb.velocity = (moveHorizontal + moveVertical) * moveSpeed;


            // View/Camera Controls
            mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity;

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
            playerCam.transform.localRotation = Quaternion.Euler(playerCam.transform.localRotation.eulerAngles - new Vector3(mouseInput.y, 0, 0));


            // Shooting

            if (damagePickup && !currentlyPoweredUp)
            {
                currentlyPoweredUp = true;
                damagePickup = false;
            }
            else if (damagePickup && currentlyPoweredUp)
            {
                currentlyPoweredUp = true;
                damagePickup = false;
                powerUpCounter = 15f;
            }
            else if (!damagePickup && currentlyPoweredUp)
            {
                PowerUpCountdown();
            }

            if (currentlyPoweredUp)
            {
                currentWeapon.weaponImage.color = Color.red;

                if (!hasDamagePerk)
                {
                    tempDamageDealt = currentWeapon.damage;
                    damageDealt = currentWeapon.damage * 2;
                }
                else if (hasDamagePerk)
                {
                    tempDamageDealt = currentWeapon.damage * 3;
                    damageDealt = currentWeapon.damage * 6;
                }
            }
            else if (!currentlyPoweredUp)
            {
                currentWeapon.weaponImage.color = Color.white;

                if (!hasDamagePerk)
                {
                    tempDamageDealt = currentWeapon.damage;
                    damageDealt = currentWeapon.damage;
                }
                else if (hasDamagePerk)
                {
                    tempDamageDealt = currentWeapon.damage * 3;
                    damageDealt = currentWeapon.damage * 3;
                }
            }

            if (!damagePickup && !currentlyPoweredUp)
            {
                if (!hasDamagePerk)
                    damageDealt = currentWeapon.damage;
                else
                    damageDealt = currentWeapon.damage * 3;
            }
            
            if (Input.GetMouseButton(0) && canFire)
            {
                if (currentWeapon.currentAmmo > 0)
                {
                    Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        //Debug.Log("I'm hitting " + hit.transform.name);
                        Instantiate(bulletImpact, hit.point, transform.rotation);

                        if (hit.transform.tag == "Enemy")
                        {
                            hit.transform.GetComponent<EnemyController>().TakeDamage(damageDealt);
                        }

                        if (hit.transform.tag == "Enemy3")
                        {
                            hit.transform.GetComponent<EnemyThirdController>().TakeDamage(damageDealt);
                        }
                    }
                    else
                    {
                        //Debug.Log("I'm hitting nothing");
                    }

                    //AudioController.instance.PlayGunshot();
                    currentWeapon.PlayGunsound();
                    currentWeapon.currentAmmo--;

                    //Shotgun animation
                    currentWeapon.uiAnimator.SetTrigger("Shoot");

                    canFire = false;
                }
            }
            

            if (!canFire)
            {
                FireRateCountdown();
            }

            ammoText.text = currentWeapon.currentAmmo.ToString();
            pointsText.text = currentPoints.ToString();

            // Bobbing animation for player
            if (moveInput != Vector3.zero)
            {
                anim.SetBool("isMoving", true);
                if (playerZone == 1)
                {
                    if (!AudioController.instance.footstepOutside.isPlaying)
                        AudioController.instance.PlayfootstepOutside();
                }
                else if (playerZone != 1)
                {
                    if (!AudioController.instance.footstepInside.isPlaying)
                        AudioController.instance.PlayfootstepInside();
                }
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
          
            // Makes the points UI start once points have been accumulated
            if (currentPoints > 0)
            {
                pointsBox.SetActive(true);
            }

        }

        // Game Over functionality

        if (playerHealth.currentHealth <= 0)
        {
            deadScreen.SetActive(true);
            totalPointsText.text = totalPoints.ToString();
            hasDied = true;
            pointsBox.SetActive(false);
            perkBox.SetActive(false);
            roundBox.SetActive(false);
            ammoBox.SetActive(false);
            currentWeapon.gameObject.SetActive(false);
        }


        if (transform.position.y >= 0.8f)
        {
            SnapToFloor();
        }
    }

    public void RewardPoints(int points)
    {
        currentPoints += points;
        totalPoints += points;
    }

    public void LosePoints(int points)
    {
        currentPoints -= points;
    }

    private void PowerUpCountdown()
    {
        powerUpCounter -= Time.deltaTime;

        if (powerUpCounter <= 0)
        {
            currentlyPoweredUp = false;
            damageDealt = tempDamageDealt;
            //shotGunImage.color = Color.white;
            currentWeapon.weaponImage.color = Color.white;
            powerUpCounter = 15f;
        }
    }

    private void FireRateCountdown()
    {
        fireRateCounter -= Time.deltaTime;

        if (fireRateCounter <= 0)
        {
            canFire = true;
            fireRateCounter = currentWeapon.firerate;
        }
    }

    private void SnapToFloor()
    {
        transform.position = new Vector3(transform.position.x, 0.75f, transform.position.z);
    }

    public void PerkUIUpdate(int perkReceived)
    {
        if (hasHealthPerk && perkReceived == healthPerkVal)
        {
            perks[perkArrayLoc] = healthPerkVal;
            perkArrayLoc++;
        }

        if (hasSpeedPerk && perkReceived == speedPerkVal)
        {
            perks[perkArrayLoc] = speedPerkVal;
            perkArrayLoc++;
        }

        if (hasDamagePerk && perkReceived == damagePerkVal)
        {
            perks[perkArrayLoc] = damagePerkVal;
            perkArrayLoc++;
        }

        if (perkArrayLoc == 1)
        {
            perkSlot1.GetComponent<Image>().sprite = perkSprites[perks[0]];
            perkBox.SetActive(true);
            perkSlot1.SetActive(true);
        }
        else if (perkArrayLoc == 2)
        {
            perkSlot2.GetComponent<Image>().sprite = perkSprites[perks[1]];
            perkSlot2.SetActive(true);
        }
        else if (perkArrayLoc == 3)
        {
            perkSlot3.GetComponent<Image>().sprite = perkSprites[perks[2]];
            perkSlot3.SetActive(true);
        }
    }
}
