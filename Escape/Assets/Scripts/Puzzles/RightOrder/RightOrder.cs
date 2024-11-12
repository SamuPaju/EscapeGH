using UnityEngine;
using UnityEngine.Events;

public class RightOrder : MonoBehaviour
{
    [SerializeField] Activator[] bottles;

    [Header("Events")]
    [SerializeField] private UnityEvent correctOrder;
    [SerializeField] private UnityEvent incorrectOrder;

    public bool puzzleIsDone = false;

    public UnityEvent OnAccessGranted => correctOrder;
    public UnityEvent OnAccessDenied => incorrectOrder;


    public void Update()
    {
        if (bottles[0].index == 3 && bottles[1].index == 1 && bottles[2].index == 6 && bottles[3].index == 5 && bottles[4].index == 4)
        {
            correctOrder.Invoke();
            puzzleIsDone = true;
        }
        else
        {
            incorrectOrder.Invoke();

        }
    }
}