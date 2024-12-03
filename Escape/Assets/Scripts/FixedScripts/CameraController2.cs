using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    [Header("Looking")]
    public float defMouseSensitivity = 100f;
    float mouseSensitivity;
    float xRotation = 0f;
    bool lookpossible = true;

    [Header("Player")]
    public Transform playerBody;
    public GameObject player;

    [Header("Raycast")]
    public float detectionRange = 5f;
    RaycastHit hit;
    RaycastHit detectionHit;

    [Header("Positions")]
    Vector3 defaultSpot;

    bool focusMode;

    [Header("UI")]
    [SerializeField] GameObject interactPossible;

    void Start()
    {
        // Lock the mouse and set the mousesensitivity
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = defMouseSensitivity;
    }

    void Update()
    {
        if (lookpossible)
        {
            Look();
        }

        // Checks if player is looking at a special object when correct button is pressed
        if (Input.GetKeyUp(KeyCode.E))
        {
            // Performs raycast to check if player is looking at object within pickuprange
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, detectionRange))
            {
                if (hit.transform.gameObject.tag == "stationPuzzle")
                {
                    StationActivator.instance.ActivateStation(1);
                }
                if (hit.transform.gameObject.GetComponent<SpotPosition>() != null)
                {
                    Focus(hit);
                }
            }
        }

        // Checks if player is looking at a special object all the time
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out detectionHit, detectionRange)) //change E to whichever key you want
        {
            if (detectionHit.transform.gameObject.tag == "stationPuzzle")
            {
                interactPossible.SetActive(true);
            }
            if (detectionHit.transform.gameObject.GetComponent<SpotPosition>() != null)
            {
                interactPossible.SetActive(true);
            }
        }
        else
        {
            interactPossible.SetActive(false);
        }

        // Goes back to normal if the player is in a focus and player presses right button
        if (focusMode)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Sets all back from Focus
                Cursor.lockState = CursorLockMode.Locked;
                lookpossible = true;
                player.GetComponentInChildren<MeshRenderer>().enabled = true;
                PlayerMovementTest.instance.ableToMove = true;
                transform.position = defaultSpot;
                transform.parent = player.transform;
                focusMode = false;
            }
        }
    }

    /// <summary>
    /// Makes it possible to look around
    /// </summary>
    void Look()
    {
        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Count the rotation
        xRotation -= mouseY;
        // Limit the rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // Set the rotation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotates camera and player horizontally
        playerBody.Rotate(Vector3.up * mouseX);
    }

    /// <summary>
    /// Sets everything ready to close inspection/interaction
    /// </summary>
    /// <param name="hit"></param>
    void Focus(RaycastHit hit)
    {
        SpotPosition spotPos = hit.transform.gameObject.GetComponent<SpotPosition>();

        // if focus mode is false go to focus
        if (!focusMode)
        {
            // Disable player body
            player.GetComponentInChildren<MeshRenderer>().enabled = false;
            // Free the mouse
            Cursor.lockState = CursorLockMode.None;
            // Disable movement and looking
            lookpossible = false;
            PlayerMovementTest.instance.ableToMove = false;
            // Unparent the camera
            transform.parent = null;
            // Set all the positions and rotations
            defaultSpot = transform.position;
            transform.position = spotPos.spot.transform.position;
            transform.rotation = spotPos.spot.transform.rotation;
            
            focusMode = true;
        }
    }
}
