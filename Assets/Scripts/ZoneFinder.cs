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
        if (other.tag == "Player" && gameObject.tag == "Zone 2")
        {
            zone = 2;
            PlayerController.instance.playerZone = zone;
        }

    }
}
