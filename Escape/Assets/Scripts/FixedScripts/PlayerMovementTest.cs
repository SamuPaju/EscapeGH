using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public static PlayerMovementTest instance;

    [Header("Movement")]
    public CharacterController controller;
    public float speed = 12f;
    public bool ableToMove = true;
    public float jumpHeight = 3f;
    public bool jumpAllowed;

    [Header("Gravity")]
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;


    void Start()
    {
        instance = this;
    }

    void Update()
    {
        // Check if the player is allowed to move
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
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Checks if spacebar is pressed, player is on the ground and jump is allowed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && jumpAllowed)
        {
            // If all are true player jumps
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
