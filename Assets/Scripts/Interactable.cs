using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool isMaxHealthPerk = false;
    [SerializeField] private bool isMoveSpeedPerk = false;
    [SerializeField] private bool isIncreasedDamagePerk = false;
    [SerializeField] private NavMeshCarve carve;
    [SerializeField] private Transform parentDoor;

    public float doorCloseOffset = -1.2f;
    private Vector3 doorCloseVectorOffset;
    public float step = 5f;
    public bool moveDoor = false;

    public GameObject contextBox;
    public Text contextText;

    public int pointCost = 1000;

    private bool hasPushedE = false;
    private float contextCounter = 0.5f;

    private PlayerController player;

    //to do : replace all PlayerController.instance areas of code with player variable above

    // Start is called before the first frame update
    void Start()
    {
        contextBox.SetActive(false);
        parentDoor = gameObject.transform.parent.gameObject.transform;

        doorCloseVectorOffset = new Vector3(parentDoor.position.x, doorCloseOffset, parentDoor.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDoor)
        {
            parentDoor.transform.position = Vector3.Lerp(parentDoor.transform.position, doorCloseVectorOffset, step * Time.deltaTime);

            if (parentDoor.transform.position == doorCloseVectorOffset)
            {
                gameObject.SetActive(false);
            }
        }

        if (gameObject.tag == "Power Door" && PowerSwitch.instance.powerOn)
        {
            moveDoor = true;
            carve.UpdateNav();
            contextBox.SetActive(false);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && gameObject.tag == "Door")
        {
            InteractDoor();
        }

        else if (other.tag == "Player" && gameObject.tag == "Perk Machine")
        {
            InteractPerk();
        }

        else if (other.tag == "Player" && gameObject.tag == "Power Door")
        {
            pointCost = 0;
            PowerDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            contextBox.SetActive(false);
        }
    }

    private void PowerDoor()
    {
        contextBox.SetActive(true);

        if (!hasPushedE)
            contextText.text = "This door can only be opened by turning on the power.";
        if (hasPushedE)
        {
            contextCounter -= Time.deltaTime;
            if (contextCounter <= 0)
            {
                hasPushedE = false;
                contextCounter = 0.5f;
            }
        }

        if (Input.GetKey(KeyCode.E) && !PowerSwitch.instance.powerOn)
        {
            hasPushedE = true;
            contextText.text = "You have not activated the power yet!";
        }
    }

    private void InteractDoor()
    {
        contextBox.SetActive(true);

        if (!hasPushedE)
            contextText.text = "Press E to open for " + pointCost.ToString() + " points.";
        if (hasPushedE)
        {
            contextCounter -= Time.deltaTime;
            if (contextCounter <= 0)
            {
                hasPushedE = false;
                contextText.text = "";
                contextBox.SetActive(false);
                contextCounter = 0.5f;
            }
        }

        if (Input.GetKey(KeyCode.E) && PlayerController.instance.currentPoints >= pointCost)
        {
            
            PlayerController.instance.currentPoints -= pointCost;
            moveDoor = true;

            carve.UpdateNav();

            
            contextBox.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.E) && PlayerController.instance.currentPoints < pointCost && !moveDoor)
        {
            hasPushedE = true;
            contextBox.SetActive(true);
            contextText.text = "You don't have enough points to open this door!";
        }
    }

    private void InteractPerk()
    {
        contextBox.SetActive(true);

        if (!hasPushedE)
            contextText.text = "Press E to drink up for " + pointCost.ToString() + " points.";
        if (hasPushedE)
        {
            contextCounter -= Time.deltaTime;
            if (contextCounter <= 0)
            {
                hasPushedE = false;
                contextCounter = 0.5f;
            }
        }

        if (Input.GetKey(KeyCode.E) && PlayerController.instance.currentPoints >= pointCost)
        {
            PlayerController.instance.currentPoints -= pointCost;
            DrinkPerk();

            gameObject.SetActive(false);
            contextBox.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.E) && PlayerController.instance.currentPoints < pointCost)
        {
            hasPushedE = true;
            contextText.text = "You don't have enough points to drink this you dingus!";
        }
    }

    private void DrinkPerk()
    {
        if (isMaxHealthPerk)
        {
            PlayerController.instance.playerHealth.maxHealth = 150;
            PlayerController.instance.playerHealth.AddHealth(150);
            PlayerController.instance.playerHealth.ExpandHealthBar();
            PlayerController.instance.hasHealthPerk = true;
            PlayerController.instance.PerkUIUpdate(PlayerController.instance.healthPerkVal);
        }
        if (isMoveSpeedPerk)
        {
            PlayerController.instance.moveSpeed = 10f;
            PlayerController.instance.hasSpeedPerk = true;
            PlayerController.instance.PerkUIUpdate(PlayerController.instance.speedPerkVal);
        }
        if (isIncreasedDamagePerk)
        {
            if (PlayerController.instance.currentlyPoweredUp)
            {
                PlayerController.instance.tempDamageDealt = 5;
                PlayerController.instance.damageDealt = 10;
            }
            else
            {
                PlayerController.instance.damageDealt = 5;
            }

            PlayerController.instance.hasDamagePerk = true;
            PlayerController.instance.PerkUIUpdate(PlayerController.instance.damagePerkVal);

            AudioController.instance.IncreaseGunshot();
        }

    }
}
