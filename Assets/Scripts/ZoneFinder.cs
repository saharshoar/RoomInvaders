using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneFinder : MonoBehaviour
{
    public int zone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameObject.tag == "Zone 1")
        {
            zone = 1;
            PlayerController.instance.playerZone = zone;
        }
        else if (other.tag == "Player" && gameObject.tag == "Zone 2")
        {
            zone = 2;
            PlayerController.instance.playerZone = zone;
        }
        else if (other.tag == "Player" && gameObject.tag == "Zone 3")
        {
            zone = 3;
            PlayerController.instance.playerZone = zone;
        }
        else if (other.tag == "Player" && gameObject.tag == "Zone 4")
        {
            zone = 4;
            PlayerController.instance.playerZone = zone;
        }
        else if (other.tag == "Player" && gameObject.tag == "Zone 5")
        {
            zone = 5;
            PlayerController.instance.playerZone = zone;
        }
        else if (other.tag == "Player" && gameObject.tag == "Zone 6")
        {
            zone = 6;
            PlayerController.instance.playerZone = zone;
        }
        else if (other.tag == "Player" && gameObject.tag == "Zone 7")
        {
            zone = 7;
            PlayerController.instance.playerZone = zone;
        }
        else if (other.tag == "Player" && gameObject.tag == "Zone 8")
        {
            zone = 8;
            PlayerController.instance.playerZone = zone;
        }
        else if (other.tag == "Player" && gameObject.tag == "Zone 9")
        {
            zone = 9;
            PlayerController.instance.playerZone = zone;
        }

    }
}
