using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCSettings : MonoBehaviour
{
    bool onOff = true;
    Vector3 startPos;
    Quaternion startRot;
    BoxCollider boxCollider;
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
    }

    public void Active()
    {
        onOff = false;
        transform.position = new Vector3(transform.position.x, startPos.y + 0.57f, startPos.z);
    }

    public void Deactivate()
    {
        onOff = true;
        transform.position = startPos;
        transform.rotation = startRot;
    }
}
