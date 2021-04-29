using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private bool gameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            PlayerController.instance.canMove = false;
            Time.timeScale = 0f;

            if (Input.GetKeyDown(KeyCode.E))
            {
                gameStarted = true;
            }
        }
        else if (gameStarted)
        {
            PlayerController.instance.canMove = true;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameObject.SetActive(false);

        }
    }
}
