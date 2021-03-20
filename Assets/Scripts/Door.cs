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
            contextText.text = "Press E to open for " + pointCost.ToString() + " points.";

            if (Input.GetKeyDown(KeyCode.E) && PlayerController.instance.currentPoints >= pointCost)
            {
                PlayerController.instance.currentPoints -= pointCost;
                doorAnim.SetBool("DoorOpened", true);

                gameObject.SetActive(false);
                contextBox.SetActive(false);
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
