using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Animator doorAnim;
    public GameObject contextBox;
    public Text contextText;

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
            contextText.text = "Press E to open for 1000 points";

            if (Input.GetKeyDown(KeyCode.E) && PlayerController.instance.currentPoints >= 1000)
            {
                PlayerController.instance.currentPoints -= 1000;
                doorAnim.SetBool("DoorOpened", true);
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
