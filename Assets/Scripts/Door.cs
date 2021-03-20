using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Animator doorAnim;
    public GameObject contextBox;
    public Text contextText;

    public int pointCost = 1000;

    private bool hasPushedE = false;
    private float contextCounter = 3f;

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
        if (other.tag == "Player")
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
                    contextCounter = 3f;
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            contextBox.SetActive(false);
        }
    }
}
