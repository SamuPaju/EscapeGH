using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public GameObject cam1;
    public GameObject cam2;
    bool camchoice = true;
    bool movingPossible = true;

    public CharacterController controller;
    public float speed = 12f;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (movingPossible)
        {
            Moving();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && camchoice == false)
            {
                print("ASDFKJLJH");
                SwitchCam();
            }
        }
    }

    /// <summary>
    /// Moving player
    /// </summary>
    void Moving()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    /// <summary>
    /// Switching camera to focus mode and player mode
    /// </summary>
    public void SwitchCam()
    {
        camchoice = !camchoice;
        movingPossible = !movingPossible;
        // Cam1
        if (camchoice)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        // Cam2
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        cam1.SetActive(camchoice);
        cam2.SetActive(!camchoice);
    }
}
