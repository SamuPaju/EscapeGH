using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player;

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
            bool isPaused = !pauseMenu.activeSelf;
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            if (isPaused)
            {
                DisablePlayerControls();
            }
            else
            {
                EnablePlayerControls();
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

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
