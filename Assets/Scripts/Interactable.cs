using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool isMaxHealthPerk = false;
    [SerializeField] private bool isMoveSpeedPerk = false;
    [SerializeField] private bool isIncreasedDamagePerk = false;

    public Animator doorAnim;
    public GameObject contextBox;
    public Text contextText;

    public int pointCost = 1000;

    private bool hasPushedE = false;
    private float contextCounter = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        contextBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            contextBox.SetActive(false);
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
                contextCounter = 0.5f;
            }
        }

        if (Input.GetKey(KeyCode.E) && PlayerController.instance.currentPoints >= pointCost)
        {
            PlayerController.instance.currentPoints -= pointCost;
            doorAnim.SetBool("DoorOpened", true);

            gameObject.SetActive(false);
            contextBox.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.E) && PlayerController.instance.currentPoints < pointCost)
        {
            hasPushedE = true;
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
        }
        if (isMoveSpeedPerk)
        {
            PlayerController.instance.moveSpeed = 10f;
        }
        if (isIncreasedDamagePerk)
        {
            PlayerController.instance.damageDealt = 5;
        }

    }
}
