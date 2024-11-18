using NavKeypad;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private RightOrder rightOrder;
    [SerializeField] private Keypad keypad;
    //[SerializeField] private
    [SerializeField] private PuzzleFour puzzleFour;


    private void Update()
    {
        FirstPuzzle();
        SecondPuzzle();
        ThirdPuzzle();
        FourPuzzle();
    }
    private void FirstPuzzle()
    {
        if (rightOrder != null && rightOrder.puzzleIsDone)
        {
            Debug.Log("here will be save in the futureeee");
        }

    }
    private void SecondPuzzle()
    {
        
    }

    private void ThirdPuzzle()
    {

    }

    private void FourPuzzle()
    {
        if (puzzleFour != null && puzzleFour.gameCompleted == true) 
        {
            
        }
    }
}
