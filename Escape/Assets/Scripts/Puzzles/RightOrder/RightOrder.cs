using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RightOrder : MonoBehaviour
{
    [SerializeField] Activator[] bottles;

    [Header("Events")]
    [SerializeField] private UnityEvent correctOrder;
    [SerializeField] private UnityEvent incorrectOrder;

    public UnityEvent OnAccessGranted => correctOrder;
    public UnityEvent OnAccessDenied => incorrectOrder;


    void Update()
    {
        if (bottles[0].index == 3 && bottles[1].index == 1 && bottles[2].index == 6 && bottles[3].index == 5 && bottles[4].index == 4)
        {
            correctOrder.Invoke();
        }
        else
        {
            incorrectOrder.Invoke();
        }
    }
}
