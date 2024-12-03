using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // References to various UI elements and player objects
    public GameObject canvas; // Pause menu canvas
    public GameObject player; // Player object
    public GameObject controls; // Controls menu
    public GameObject hints; // Hints menu
    public GameObject pause; // Pause menu content

    // References to puzzle hints
    public GameObject puzzle1;
    public GameObject puzzle2;
    public GameObject puzzle3;
    public GameObject puzzle4;

    // References to player movement and camera control scripts
    private PlayerMovementTest playerMovement;
    private CameraController2 cameraController;

    private bool wasCursorVisible;
    private CursorLockMode previousCursorLockState;

    private void Start()
    {
        // Initialize references to player movement and camera controller scripts
        playerMovement = player.GetComponent<PlayerMovementTest>();
        cameraController = player.GetComponentInChildren<CameraController2>();
    }

    private void Update()
    {
        // Toggle pause menu when Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isPaused = !canvas.activeSelf;

            if (isPaused)
            {
                
                wasCursorVisible = Cursor.visible;
                previousCursorLockState = Cursor.lockState;

                DisablePlayerControls();
                ShowCursor(); 
            }
            else
            {
                EnablePlayerControls();
                RestoreCursorState(); 
            }

            canvas.SetActive(isPaused);
        }
    }

    // Disable player movement and camera control
    private void DisablePlayerControls()
    {
        if (playerMovement != null) playerMovement.enabled = false;
        if (cameraController != null) cameraController.enabled = false;
    }

    // Enable player movement and camera control
    private void EnablePlayerControls()
    {
        if (playerMovement != null) playerMovement.enabled = true;
        if (cameraController != null) cameraController.enabled = true;
    }

    // Show the mouse cursor and unlock it
    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void RestoreCursorState()
    {
        Cursor.visible = wasCursorVisible;
        Cursor.lockState = previousCursorLockState;
    }

    // Load the main menu scene
    public void MainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    // Show the controls menu and hide the pause menu
    public void Controls()
    {
        controls.SetActive(true);
        pause.SetActive(false);
    }

    // Show the hints menu and hide the pause menu
    public void Hints()
    {
        hints.SetActive(true);
        pause.SetActive(false);
    }

    // Return to the pause menu from hints or controls menu
    public void Back()
    {
        if (hints.activeSelf)
        {
            hints.SetActive(false);
            pause.SetActive(true);
        }

        if (controls.activeSelf)
        {
            controls.SetActive(false);
            pause.SetActive(true);
        }
    }

    // Show Puzzle 1 menu and hide the hints menu
    public void Puzzle1()
    {
        if (hints.activeSelf)
        {
            hints.SetActive(false);
            puzzle1.SetActive(true);
        }
    }

    // Show Puzzle 2 menu and hide the hints menu
    public void Puzzle2()
    {
        if (hints.activeSelf)
        {
            hints.SetActive(false);
            puzzle2.SetActive(true);
        }
    }

    // Show Puzzle 3 menu and hide the hints menu
    public void Puzzle3()
    {
        if (hints.activeSelf)
        {
            hints.SetActive(false);
            puzzle3.SetActive(true);
        }
    }

    // Show Puzzle 4 menu and hide the hints menu
    public void Puzzle4()
    {
        if (hints.activeSelf)
        {
            hints.SetActive(false);
            puzzle4.SetActive(true);
        }
    }

    // Return from any puzzle menu to the hints menu
    public void BackFromPuzzleToHints()
    {
        if (puzzle1.activeSelf || puzzle2.activeSelf || puzzle3.activeSelf || puzzle4.activeSelf)
        {
            puzzle1.SetActive(false);
            puzzle2.SetActive(false);
            puzzle3.SetActive(false);
            puzzle4.SetActive(false);
            hints.SetActive(true);
        }
    }
}
