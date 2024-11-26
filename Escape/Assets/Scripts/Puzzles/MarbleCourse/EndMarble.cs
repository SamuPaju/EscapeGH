using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMarble : MonoBehaviour
{
    [SerializeField] public bool puzzleDone = false;
    private void OnTriggerEnter(Collider other)
    {
        puzzleDone = true;
    }
}
