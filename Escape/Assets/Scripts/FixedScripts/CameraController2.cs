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
    RaycastHit hit;
    RaycastHit detectionHit;
    public float detectionRange = 5f;

    [Header("Positions")]
    Vector3 defaultSpot;
    bool backNormal;

    [Header("UI")]
    [SerializeField] GameObject interactPossible;

    void Start()
    {
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
        if (Input.GetKeyUp(KeyCode.E)) //change E to whichever key you want
        {
            //perform raycast to check if player is looking at object within pickuprange
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
        if (backNormal)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Cursor.lockState = CursorLockMode.Locked;
                lookpossible = true;
                player.GetComponentInChildren<MeshRenderer>().enabled = true;
                PlayerMovementTest.instance.ableToMove = true;
                transform.position = defaultSpot;
                transform.parent = player.transform;
                backNormal = false;
            }
        }
    }

    // Makes it possible to look around
    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // Sets everything ready to close inspection/interaction
    void Focus(RaycastHit hit)
    {
        SpotPosition spotPos = hit.transform.gameObject.GetComponent<SpotPosition>();

        if (!backNormal)
        {
            player.GetComponentInChildren<MeshRenderer>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            lookpossible = false;
            PlayerMovementTest.instance.ableToMove = false;
            transform.parent = null;
            defaultSpot = transform.position;
            transform.position = spotPos.spot.transform.position;
            transform.rotation = spotPos.spot.transform.rotation;
            backNormal = true;
        }
    }
}
