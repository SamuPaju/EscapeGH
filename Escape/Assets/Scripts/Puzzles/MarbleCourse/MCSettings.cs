using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCSettings : MonoBehaviour
{
    BoxCollider boxCollider;
    [SerializeField] Transform cam;

    Vector3 startPos;
    Quaternion startRot;

    Vector3 marbleStartPos;
    [SerializeField] GameObject marble;
    
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        startPos = transform.localPosition;
        startRot = transform.rotation;
        marbleStartPos = marble.transform.localPosition;
    }

    void Update()
    {
        Rotate();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Deactivate();
        }
    }

    public void Active()
    {
        marble.SetActive(true);
    }

    public void Deactivate()
    {
        Restart();
        StationActivator.instance.QuitStation(1);
    }

    void Restart()
    {
        transform.localPosition = startPos;
        transform.rotation = startRot;
        marble.transform.localPosition = marbleStartPos;
        marble.GetComponent<Rigidbody>().isKinematic = true;
        marble.GetComponent<Rigidbody>().isKinematic = false;
    }

    void Rotate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (cam.transform.rotation.eulerAngles.y >= 45 && cam.transform.rotation.eulerAngles.y < 135)
        {
            transform.Rotate(Vector3.left, horizontalInput);
            transform.Rotate(Vector3.back, verticalInput);
        }
        if (cam.transform.rotation.eulerAngles.y >= 135 && cam.transform.rotation.eulerAngles.y < 225)
        {
            transform.Rotate(Vector3.forward, horizontalInput);
            transform.Rotate(Vector3.left, verticalInput);
        }
        if (cam.transform.rotation.eulerAngles.y >= 225 && cam.transform.rotation.eulerAngles.y < 315)
        {
            transform.Rotate(Vector3.right, horizontalInput);
            transform.Rotate(Vector3.forward, verticalInput);
        }
        if (cam.transform.rotation.eulerAngles.y >= 315 && cam.transform.rotation.eulerAngles.y < 360 || cam.transform.rotation.eulerAngles.y >= 0 && cam.transform.rotation.eulerAngles.y < 45)
        {
            transform.Rotate(Vector3.back, horizontalInput);
            transform.Rotate(Vector3.right, verticalInput);
        }
    }
}
