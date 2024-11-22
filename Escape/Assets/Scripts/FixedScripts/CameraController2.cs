using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    public float defMouseSensitivity = 100f;
    float mouseSensitivity;
    public Transform playerBody;
    float xRotation = 0f;
    RaycastHit hit;
    bool lookpossible = true;
    [SerializeField] bool stationMode = false;
    Vector3 defaultSpot;
    bool backNormal;
    public GameObject player;
    public float pickUpRange = 5f;
    private GameObject heldObj;
    private Rigidbody heldObjRb;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = defMouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookpossible)
        {
            Look();
        }

        if (Input.GetKeyUp(KeyCode.E)) //change E to whichever key you want to press to pick up
        {
            
            //perform raycast to check if player is looking at object within pickuprange
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
            {
                if (hit.transform.gameObject.tag == "stationPuzzle")
                {
                    //PickUpForStations(hit.transform.gameObject);
                    //hit.transform.gameObject.GetComponent<MCSettings>().Active();
                    StationActivator.instance.ActivateStation(1);
                }
                if (hit.transform.gameObject.GetComponent<SpotPosition>() != null)
                {
                    Focus(hit);
                }
            }
        }
        if (stationMode == true)
        {
            //BetterRotate();
        }

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
                /*if (stationMode == true)
                {
                    DropForStation();
                    hit.transform.gameObject.GetComponent<MCSettings>().Deactivate();
                    stationMode = false;
                }*/
            }
        }
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void Focus(RaycastHit hit)
    {
        //PlayerMovementTest PMT = player.GetComponent<PlayerMovementTest>();
        SpotPosition spotPos = hit.transform.gameObject.GetComponent<SpotPosition>();

        if (!backNormal)
        {
            player.GetComponentInChildren<MeshRenderer>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            //mouseSensitivity = 0f;
            lookpossible = false;
            PlayerMovementTest.instance.ableToMove = false;
            transform.parent = null;
            defaultSpot = transform.position;
            transform.position = spotPos.spot.transform.position;
            transform.rotation = spotPos.spot.transform.rotation;
            backNormal = true;
        }
    }

    /*void PickUpForStations(GameObject pickUpObj)
    {
        heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
        heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
        heldObjRb.isKinematic = true;
        //make sure object doesnt collide with player, it can cause weird bugs
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        stationMode = true;
    }

    void DropForStation()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; //object assigned back to default layer
        heldObjRb.isKinematic = false;
        //heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
    }

    void BetterRotate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        heldObj.transform.Rotate(Vector3.back, horizontalInput);
        heldObj.transform.Rotate(Vector3.right, verticalInput);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            heldObj.transform.Rotate(Vector3.down, 90);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            heldObj.transform.Rotate(Vector3.up, 90);
        }
    }*/
}
