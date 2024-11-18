using UnityEngine;
using UnityEngine.Events;

public class PuzzleFour : MonoBehaviour
{
    public GameObject[] cubes; // All cubes are here
    private Vector3[] positions; // Save cubes position
    private Vector3 emptySpace; // Empty spot position
    public bool gameCompleted = false; // Is game done
    public LayerMask cubeLayer; // Layer for cubes

    [SerializeField] private UnityEvent done;
    public UnityEvent Done => done;

    void Start()
    {
        positions = new Vector3[16];

        // Saving cubes local positions
        for (int i = 0; i < cubes.Length; i++)
        {
            positions[i] = cubes[i].transform.localPosition;
        }

        // Empty spot position
        emptySpace = new Vector3(0.75f, 0f, -0.75f);
    }

    void Update()
    {
        if (gameCompleted) return; // If game is done, stoppppppp

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, cubeLayer))
            {
                GameObject selectedCube = hit.collider.gameObject;

                // Check is it cube
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
    }

    bool CanMoveCube(GameObject cube)
    {
        Vector3 cubePos = cube.transform.localPosition;

        return (Mathf.Approximately(Mathf.Abs(cubePos.x - emptySpace.x), 0.25f) && Mathf.Approximately(cubePos.z, emptySpace.z)) ||
               (Mathf.Approximately(Mathf.Abs(cubePos.z - emptySpace.z), 0.25f) && Mathf.Approximately(cubePos.x, emptySpace.x));
    }
    //Moving cube
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
            // Right position for every cube
            Vector3 correctPosition = new Vector3(
                (i % 4) * 0.25f,  // X
                cubes[i].transform.localPosition.y, // Y
                -(Mathf.Floor(i / 4) * 0.25f)       // Z
            );

            Vector3 currentPosition = cubes[i].transform.localPosition;

            // Check everything to not have a mistakes
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
}
