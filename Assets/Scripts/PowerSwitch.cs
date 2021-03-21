using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSwitch : MonoBehaviour
{
    public static PowerSwitch instance;

    public GameObject contextBox;
    public Text contextText;

    public bool powerOn = false;
    private float contextCounter = 0.5f;
    private bool hasPushedE = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && gameObject.tag == "Power Switch")
        {
            InteractPower();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            contextBox.SetActive(false);
        }
    }

    private void InteractPower()
    {
        contextBox.SetActive(true);

        if (!powerOn)
            contextText.text = "Press E to activate power!";

        if (!hasPushedE && powerOn)
            contextBox.SetActive(false);
        if (hasPushedE)
        {
            contextBox.SetActive(true);

            contextCounter -= Time.deltaTime;
            if (contextCounter <= 0)
            {
                hasPushedE = false;
                contextCounter = 0.5f;
            }
        }

        if (Input.GetKey(KeyCode.E) && !powerOn)
        {
            powerOn = true;
        }
        else if (Input.GetKey(KeyCode.E) && powerOn)
        {
            hasPushedE = true;
            contextText.text = "You have already activated the power.";
        }
    }
}
