using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float defMouseSensitivity = 100f;
    float mouseSensitivity;
    public Transform playerBody;
    float xRotation = 0f;
    Camera cam;
    Vector3 defaultSpot;

    bool backNormal;
    bool lookpossible = true;
    [SerializeField] bool stationMode = false;
    RaycastHit hit;

    public GameObject player;
    public Transform holdPos;
    //if you copy from below this point, you are legally required to like the video
    public float throwForce = 500f; //force at which the object is thrown at
    public float pickUpRange = 5f; //how far the player can pickup the object from
    private float rotationSensitivity = 1f; //how fast/slow the object is rotated in relation to mouse movement
    private GameObject heldObj; //object which we pick up
    private Rigidbody heldObjRb; //rigidbody of object we pick up
    private bool canDrop = true; //this is needed so we don't throw/drop object when rotating the object
    private int LayerNumber; //layer index


    private void Awake() => cam = Camera.main;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = defMouseSensitivity;
        

        LayerNumber = LayerMask.NameToLayer("holdLayer"); //if your holdLayer is named differently make sure to change this ""
    }

    void Update()
    {
        if (lookpossible)
        {
            Look();
        }

        if (Input.GetKeyUp(KeyCode.E)) //change E to whichever key you want to press to pick up
        {
            if (heldObj == null) //if currently not holding anything
            {
                //perform raycast to check if player is looking at object within pickuprange
                //RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    //make sure pickup tag is attached
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        //pass in object hit into the PickUpObject function
                        PickUpObject(hit.transform.gameObject);
                    }
                    if (hit.transform.gameObject.GetComponent<SpotPosition>() != null)
                    {
                        Focus(hit);
                        if (hit.transform.gameObject.tag == "stationPickUp")
                        {
                            PickUpForStations(hit.transform.gameObject);
                            hit.transform.gameObject.GetComponent<MCSettings>().Active();
                        }
                    }
                }
            }
            else
            {
                if (canDrop == true && stationMode == false)
                {
                    StopClipping(); //prevents object from clipping through walls
                    DropObject();
                }
            }
        }
        if (heldObj != null && stationMode == false) //if player is holding object
        {
            MoveObject(); //keep object position at holdPos
            RotateObject();
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true) //Mous0 (leftclick) is used to throw, change this if you want another button to be used)
            {
                StopClipping();
                ThrowObject();
            }
        }
        if (stationMode == true)
        {
            BetterRotate();
        }

        if (backNormal)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Cursor.lockState = CursorLockMode.Locked;
                //mouseSensitivity = defMouseSensitivity;
                lookpossible = true;
                player.GetComponentInChildren<MeshRenderer>().enabled = true;
                PlayerMovementTest.instance.ableToMove = true;
                transform.position = defaultSpot;
                transform.parent = player.transform;
                backNormal = false;
                if (stationMode == true)
                {
                    StopClipping(); //prevents object from clipping through walls
                    DropForStation();
                    hit.transform.gameObject.GetComponent<MCSettings>().Deactivate();
                    stationMode = false;
                }
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

    /// <summary>
    /// Makes camera focused on a station
    /// 0 = 0(Eteen), 90 = 0(Eteen), 180 = 90 (Oikealle), 270 = 180(Taakse), 360 = -90(Vasemmalle)
    /// </summary>
    /// <param name="hit"></param>
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

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
            heldObj.layer = LayerNumber; //change the object layer to the holdLayer
            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    /// <summary>
    /// Pickup for station objects
    /// </summary>
    /// <param name="pickUpObj"></param>
    void PickUpForStations(GameObject pickUpObj)
    {
        heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
        heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
        heldObjRb.isKinematic = true;
        //heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
        heldObj.layer = LayerNumber; //change the object layer to the holdLayer
        //make sure object doesnt collide with player, it can cause weird bugs
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        stationMode = true;
    }

    void DropObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; //object assigned back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
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

    void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }
    void RotateObject()
    {
        if (Input.GetKey(KeyCode.R))//hold R key to rotate, change this to whatever key you want
        {
            canDrop = false; //make sure throwing can't occur during rotating

            //disable player being able to look around
            mouseSensitivity = 0f;

            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
            //rotate the object depending on mouse X-Y Axis
            heldObj.transform.Rotate(Vector3.down, XaxisRotation);
            heldObj.transform.Rotate(Vector3.right, YaxisRotation);
        }
        else
        {
            //re-enable player being able to look around
            mouseSensitivity = defMouseSensitivity;
            canDrop = true;
        }
    }

    void BetterRotate()
    {
        canDrop = false;

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
    }

    void ThrowObject()
    {
        //same as drop function, but add force to object before undefining it
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }
    void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //change object position to camera position 
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }
}
