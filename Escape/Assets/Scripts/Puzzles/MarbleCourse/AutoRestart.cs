using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRestart : MonoBehaviour
{
    [SerializeField] MCSettings marbleCourse;

    /// <summary>
    /// If marble enters this objects boxcollider it restarts the MarbleCourse
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("marble"))
        {
            marbleCourse.Restart();
        }
    }
}
