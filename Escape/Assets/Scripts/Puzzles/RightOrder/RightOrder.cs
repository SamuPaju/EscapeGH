using UnityEngine;
using UnityEngine.Events;

public class RightOrder : MonoBehaviour
{
    [SerializeField] Activator[] bottles;

    [Header("Events")]
    [SerializeField] public UnityEvent correctOrder;
    [SerializeField] private UnityEvent incorrectOrder;

    public bool puzzleIsDone = false;

    public UnityEvent OnGranted => correctOrder;
    public UnityEvent OnDenied => incorrectOrder;

    public void Update()
    {
        // Checks if the bottles have correct colors (31654)
        if (bottles[0].index == 3 && bottles[1].index == 1 && bottles[2].index == 6 && bottles[3].index == 5 && bottles[4].index == 4)
        {
            if (!puzzleIsDone) // Is puzzle done
            {
                // Invokes a UnityEvent and marks the puzzle done
                correctOrder.Invoke();
                puzzleIsDone = true;
            }
        }
        else
        {
            incorrectOrder.Invoke();
        }
    }
}
