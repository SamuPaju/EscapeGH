using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRestart : MonoBehaviour
{
    [SerializeField] MCSettings marbleCourse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("marble"))
        {
            marbleCourse.Restart();
        }
    }
}
