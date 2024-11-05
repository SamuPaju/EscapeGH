using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSensor : MonoBehaviour
{
    FocusMode fm;

    void Start()
    {
        fm = GetComponentInParent<FocusMode>();
    }


    private void OnTriggerEnter(Collider other)
    {
        fm.inArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        fm.inArea = false;
    }
}
