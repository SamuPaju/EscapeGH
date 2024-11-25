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

    private StartGame startGame;

    private void Start()
    {
        // Load puzzle state when game start 
        LoadPuzzleState();
    }

    public void Update()
    {
        if (bottles[0].index == 3 && bottles[1].index == 1 && bottles[2].index == 6 && bottles[3].index == 5 && bottles[4].index == 4)
        {
            if (!puzzleIsDone) // Is puzzle done
            {
                correctOrder.Invoke();
                puzzleIsDone = true;

                // Save puzzle state
                SavePuzzleState();
            }
        }
        else
        {
            incorrectOrder.Invoke();
        }
    }

    private void SavePuzzleState()
    {
        PlayerPrefs.SetInt("PuzzleCompleted", puzzleIsDone ? 1 : 0); // Save
        PlayerPrefs.Save();
        Debug.Log("Puzzle state saved.");
    }

    private void LoadPuzzleState()
    {
        if (PlayerPrefs.HasKey("PuzzleCompleted"))
        {
            puzzleIsDone = PlayerPrefs.GetInt("PuzzleCompleted") == 1;

            if (puzzleIsDone)
            {
                // If save call corectOrder metod
                correctOrder.Invoke();
                //startGame.Welcome?.Invoke();

                Debug.Log("Puzzle state loaded: Completed");
            }
            else
            {
                Debug.Log("Puzzle state loaded: Not Completed");
            }
        }
        else
        {
            Debug.Log("No saved puzzle state found.");
        }
    }
}
