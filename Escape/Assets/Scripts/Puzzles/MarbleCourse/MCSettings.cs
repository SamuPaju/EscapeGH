using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCSettings : MonoBehaviour
{
    bool onOff = true;
    BoxCollider boxCollider;

    Vector3 startPos;
    Quaternion startRot;
    Vector3 startPosHeld;

    Vector3 marbleStartPos;
    [SerializeField] GameObject marble;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        boxCollider.enabled = onOff;

        if (onOff == false)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }
    }

    public void Active()
    {
        onOff = false;
        transform.position = new Vector3(transform.position.x, startPos.y + 0.57f, startPos.z);
        marble.SetActive(true);
        startPosHeld = transform.position;
        marbleStartPos = marble.transform.position;
    }

    public void Deactivate()
    {
        onOff = true;
        transform.position = startPos;
        transform.rotation = startRot;
        marble.SetActive(false);
    }

    void Restart()
    {
        transform.position = startPosHeld;
        transform.rotation = startRot;
        marble.transform.position = marbleStartPos;
        marble.GetComponent<Rigidbody>().isKinematic = true;
        marble.GetComponent<Rigidbody>().isKinematic = false;
    }
}
