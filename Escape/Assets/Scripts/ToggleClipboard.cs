using UnityEngine;
using TMPro;

public class ToggleClipboard : MonoBehaviour
{
    [SerializeField] private GameObject clipboardObject; // The clipboard object
    [SerializeField] private TextMeshProUGUI textArea;   // Text area on the clipboard
    [SerializeField] private GameObject player;         // Player object (for freezing movement)
    [SerializeField] private float distanceFromCamera = 0.7f; // Distance from the camera to the clipboard

    private PlayerMovementTest playerMovement;          // Player movement script
    private CameraController2 cameraController;         // Camera controller script
    private bool isClipboardActive = false;             // Clipboard visibility state
    private bool wasCursorVisible;                      // To save cursor visibility state
    private CursorLockMode previousCursorLockState;     // To save cursor lock state

    // Parameters for text restrictions
    [SerializeField] private int maxCharactersPerLine = 13; // Maximum characters per line

    private void Start()
    {
        // Initialize movement and camera control scripts
        playerMovement = player.GetComponent<PlayerMovementTest>();
        cameraController = player.GetComponentInChildren<CameraController2>();
    }

    private void Update()
    {
        // Toggle clipboard visibility when Tab is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleClipboardObject();
        }

        // Handle text input if the clipboard is active
        if (isClipboardActive && Input.anyKeyDown)
        {
            HandleTextInput();
        }
    }

    private void ToggleClipboardObject()
    {
        // Toggle clipboard visibility
        isClipboardActive = !isClipboardActive;
        clipboardObject.SetActive(isClipboardActive);

        if (isClipboardActive)
        {
            // Save cursor state and disable player controls
            wasCursorVisible = Cursor.visible;
            previousCursorLockState = Cursor.lockState;

            DisablePlayerControls();
            ShowCursor();

            // Position the clipboard in front of the camera
            PositionClipboardInFrontOfCamera();
        }
        else
        {
            // Restore player controls and cursor state
            EnablePlayerControls();
            RestoreCursorState();
        }
    }

    private void PositionClipboardInFrontOfCamera()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Position the clipboard in front of the camera
            clipboardObject.transform.position = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;

            // Rotate the clipboard to face the camera
            clipboardObject.transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward, Vector3.up);

            // Additional rotation adjustment if the clipboard orientation is incorrect
            clipboardObject.transform.Rotate(new Vector3(90, 180, 0)); // Example adjustment
        }
        else
        {
            Debug.LogWarning("Main camera not found. Clipboard positioning failed.");
        }
    }

    private void HandleTextInput()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // Handle Backspace
            {
                if (textArea.text.Length > 0)
                {
                    textArea.text = textArea.text.Substring(0, textArea.text.Length - 1);
                }
            }
            else if (c == '\n' || c == '\r') // Handle Enter/Return
            {
                AddNewLine();
            }
            else
            {
                AddCharacter(c);
            }
        }
    }

    private void AddNewLine()
    {
        // Split text into lines
        string[] lines = textArea.text.Split('\n');

        // Add a new line only if the total number of lines is less than 10
        if (lines.Length < 10)
        {
            textArea.text += "\n";
        }
    }

    private void AddCharacter(char c)
    {
        // Check total character count
        if (textArea.text.Length >= 120)
        {
            return; // Stop adding text if the limit is reached
        }

        string[] lines = textArea.text.Split('\n');

        // Add a new line if the current line exceeds the maximum characters and total lines are less than 10
        if (lines.Length > 0 && lines[lines.Length - 1].Length >= maxCharactersPerLine)
        {
            AddNewLine();
        }

        // Add the character to the text area only if total lines are less than 10
        if (lines.Length < 10)
        {
            textArea.text += c;
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

    private void RestoreCursorState()
    {
        Cursor.visible = wasCursorVisible;
        Cursor.lockState = previousCursorLockState;
    }
}
