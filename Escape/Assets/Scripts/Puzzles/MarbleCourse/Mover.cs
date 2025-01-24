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
        // Check which movement object should be doing
        if (moving)
        {
            MoveObject();
        }
        if (spinning)
        {
            SpinObject();
        }
    }

    /// <summary>
    /// Moves object back and forward (useless at the moment and not completed)
    /// </summary>
    void MoveObject()
    {
        //---Works one way---
        //transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    /// <summary>
    /// Spins object on certain axis
    /// </summary>
    void SpinObject()
    {
        // Check which direction object should be rotating
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
