using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Moving platform variables")]
    [SerializeField] bool moving;

    [Header("Spinning variables")]
    [SerializeField] bool spinning;
    [SerializeField] bool spinVecUp;
    [SerializeField] bool spinVecRight;
    // + rotates clockwise, - rotates counterclockwise
    [SerializeField] float speed;

    void Update()
    {
        if (moving)
        {
            MoveObject();
        }
        if (spinning)
        {
            SpinObject();
        }
    }

    void MoveObject()
    {
        //---Works one way---
        //transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    void SpinObject()
    {
        if (spinVecUp == true)
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        if (spinVecRight == true)
        {
            transform.Rotate(Vector3.right * speed * Time.deltaTime);
        }
    }

}
