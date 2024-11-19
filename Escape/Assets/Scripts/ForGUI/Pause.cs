using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public GameObject controls;
    public GameObject hints;
    public GameObject pause;

    private PlayerMovementTest playerMovement;
    private CameraController cameraController;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovementTest>();
        cameraController = player.GetComponentInChildren<CameraController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isPaused = !canvas.activeSelf;
            canvas.SetActive(!canvas.activeSelf);

            if (isPaused)
            {
                DisablePlayerControls();
                ShowCursor();
            }
            else
            {
                EnablePlayerControls();
                HideCursor();
            }
        }
    }

    private void DisablePlayerControls()
    {
        if (playerMovement != null) playerMovement.enabled = false;
        if (cameraController != null) cameraController.enabled = false;
    }

    private void EnablePlayerControls()
    {
        if (playerMovement != null) playerMovement.enabled = true;
        if (cameraController != null) cameraController.enabled = true;
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void Controls()
    {
        controls.SetActive(true);
        pause.SetActive(false);
    }

    public void Hints()
    {
        hints.SetActive(true);
        pause.SetActive(false);
    }

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
}
