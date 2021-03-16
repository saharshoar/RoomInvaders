﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // To do: Rearrange variables based on use

    public static PlayerController instance;

    private Rigidbody playerRb;

    public float moveSpeed = 6f;
    public float mouseSensitivity = 4f;

    private Vector3 moveInput;
    private Vector2 mouseInput;

    public Camera playerCam;

    public GameObject bulletImpact;
    public Animator shotGunUI;

    public int currentAmmo = 10;
    public int damageDealt = 1;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
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

        if (Input.GetMouseButtonDown(0))
        {
            if (currentAmmo > 0)
            {
                Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //Debug.Log("I'm hitting " + hit.transform.name);
                    Instantiate(bulletImpact, hit.point, transform.rotation);

                    if(hit.transform.tag == "Enemy")
                    {
                        hit.transform.GetComponent<EnemyController>().TakeDamage(damageDealt);
                    }
                }
                else
                    Debug.Log("I'm hitting nothing");

                currentAmmo--;

                //Shotgun animation
                shotGunUI.SetTrigger("Shoot");
            }
        }

    }
}
