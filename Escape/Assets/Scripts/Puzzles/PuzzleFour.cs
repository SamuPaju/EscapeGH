using UnityEngine;
using UnityEngine.Events;

public class PuzzleFour : MonoBehaviour
{
    public GameObject[] cubes; // All cubes are here
    private Vector3[] initialPositions; // Save initial cubes positions
    private Vector3 emptySpace; // Empty spot position
    private Vector3 initialEmptySpace; // Initial empty spot position
    public bool gameCompleted = false; // Is game done
    public LayerMask cubeLayer; // Layer for cubes

    // Opening final (exit) door when puzzle is done
    [SerializeField] public UnityEvent done;
    public UnityEvent Done => done;

    void Start()
    {
        // Initialize positions arrays
        initialPositions = new Vector3[16];

        // Save initial positions of cubes
        for (int i = 0; i < cubes.Length; i++)
        {
            initialPositions[i] = cubes[i].transform.localPosition;
        }

        // Save the initial empty space position
        initialEmptySpace = new Vector3(0.75f, 0f, -0.75f);

        // Initialize current empty space
        emptySpace = initialEmptySpace;
    }

    void Update()
    {
        if (gameCompleted) return; // If game is done, stop

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, cubeLayer))
            {
                GameObject selectedCube = hit.collider.gameObject;

                // Check if it's a cube
                if (System.Array.Exists(cubes, cube => cube == selectedCube))
                {
                    if (CanMoveCube(selectedCube))
                    {
                        MoveCube(selectedCube);

                        if (IsPuzzleComplete())
                        {
                            gameCompleted = true;
                            done.Invoke();
                        }
                    }
                }
            }
        }

        // Restart puzzle on 'R' press
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartPuzzle();
        }
    }

    bool CanMoveCube(GameObject cube)
    {
        Vector3 cubePos = cube.transform.localPosition;

        return (Mathf.Approximately(Mathf.Abs(cubePos.x - emptySpace.x), 0.25f) && Mathf.Approximately(cubePos.z, emptySpace.z)) ||
               (Mathf.Approximately(Mathf.Abs(cubePos.z - emptySpace.z), 0.25f) && Mathf.Approximately(cubePos.x, emptySpace.x));
    }

    // Moving cube
    void MoveCube(GameObject cube)
    {
        Vector3 cubePos = cube.transform.localPosition;
        cube.transform.localPosition = new Vector3(emptySpace.x, emptySpace.y, emptySpace.z);
        emptySpace = cubePos;
    }

    bool IsPuzzleComplete()
    {
        bool allInPlace = true;

        for (int i = 0; i < cubes.Length; i++)
        {
            // Correct position for every cube
            Vector3 correctPosition = new Vector3(
                (i % 4) * 0.25f,  // X
                cubes[i].transform.localPosition.y, // Y
                -(Mathf.Floor(i / 4) * 0.25f)       // Z
            );

            Vector3 currentPosition = cubes[i].transform.localPosition;

            // Check everything to not have mistakes
            bool xMatch = Mathf.Approximately(currentPosition.x, correctPosition.x);
            bool yMatch = Mathf.Approximately(currentPosition.y, correctPosition.y);
            bool zMatch = Mathf.Approximately(currentPosition.z, correctPosition.z);

            if (!xMatch || !yMatch || !zMatch)
            {
                allInPlace = false;
            }
        }

        return allInPlace;
    }

    // Reset puzzle to its initial state
    void RestartPuzzle()
    {
        // Reset all cubes to their initial positions
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].transform.localPosition = initialPositions[i];
        }

        // Reset empty space to its initial position
        emptySpace = initialEmptySpace;

        // Reset gameCompleted flag
        gameCompleted = false;

        Debug.Log("Puzzle restarted!");
    }
}
