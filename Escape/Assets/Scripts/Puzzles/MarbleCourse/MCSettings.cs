using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCSettings : MonoBehaviour
{
    [SerializeField] Transform cam;

    Vector3 startPos;
    Quaternion startRot;

    Vector3 marbleStartPos;
    [SerializeField] GameObject marble;
    
    void Start()
    {
        startPos = transform.localPosition;
        startRot = transform.rotation;
        marbleStartPos = marble.transform.localPosition;
    }

    void Update()
    {
        Rotate();

        // R restarts
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        // F quits
        if (Input.GetKeyDown(KeyCode.F))
        {
            Deactivate();
        }

        // Pressing L wins the course (for testing purposes)
        if (Input.GetKeyDown(KeyCode.L))
        {
            marble.transform.localPosition = new Vector3(0.2391f, 0.492f, -0.2114f);
        }
    }

    /// <summary>
    /// Activates the game
    /// </summary>
    public void Active()
    {
        marble.SetActive(true);
    }

    /// <summary>
    /// Deactivates the game
    /// </summary>
    public void Deactivate()
    {
        Restart();
        StationActivator.instance.QuitStation(1);
    }

    /// <summary>
    /// Resets the game
    /// </summary>
    public void Restart()
    {
        transform.localPosition = startPos;
        transform.rotation = startRot;
        marble.transform.localPosition = marbleStartPos;
        marble.GetComponent<Rigidbody>().isKinematic = true;
        marble.GetComponent<Rigidbody>().isKinematic = false;
    }

    /// <summary>
    /// Rotates the platform based on the cmaera rotations
    /// </summary>
    void Rotate()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * 0.5f;
        float verticalInput = Input.GetAxis("Vertical") * 0.5f;

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
