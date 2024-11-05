using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public static PlayerMovementTest instance;

    public CharacterController controller;
    public float speed = 12f;
    public bool ableToMove = true;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (ableToMove)
        {
            Moving();
        }
    }


    /// <summary>
    /// Moving player
    /// </summary>
    public void Moving()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }
}
