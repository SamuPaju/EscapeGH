using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndMarble : MonoBehaviour
{
    [SerializeField] private UnityEvent goal;
    public UnityEvent Goal => goal;
    [SerializeField] public bool puzzleDone = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("marble"))
        {
            puzzleDone = true;
            StationActivator.instance.QuitStation(2);
            other.gameObject.SetActive(false);
            goal.Invoke();
        }
    }
}
