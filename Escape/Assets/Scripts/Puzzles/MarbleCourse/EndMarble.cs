using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndMarble : MonoBehaviour
{
    [SerializeField] public UnityEvent goal;
    public UnityEvent Goal => goal;
    [SerializeField] public bool puzzleDone = false;

    private void OnTriggerEnter(Collider other)
    {
        // If marble hits this objects BoxCollider player wins
        if (other.gameObject.CompareTag("marble"))
        {
            puzzleDone = true;
            StationActivator.instance.QuitStation(2);
            other.gameObject.SetActive(false);
            goal.Invoke();
        }
    }
}
