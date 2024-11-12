using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private RightOrder rightOrder;

    private void Update()
    {
        FirstPuzzle();
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

    private void FinalPuzzle()
    {

    }
}
