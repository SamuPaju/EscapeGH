using UnityEngine;
using TMPro;

public class ToggleClipboard : MonoBehaviour
{
    [SerializeField] private GameObject clipboardObject; // The clipboard object
    [SerializeField] private TextMeshProUGUI textArea;   // Text area on the clipboard
    [SerializeField] private GameObject player;         // Player object (for freezing movement)
    [SerializeField] private float distanceFromCamera = 0.7f; // Distance from the camera to the clipboard
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject pause;

    private PlayerMovementTest playerMovement;          // Player movement script
    private CameraController2 cameraController;         // Camera controller script
    private bool isClipboardActive = false;             // Clipboard visibility state
    private bool wasCursorVisible;                      // To save cursor visibility state
    private CursorLockMode previousCursorLockState;     // To save cursor lock state

    private string textContent = "";                   // The text content being edited
    private int cursorPosition = 0;                     // Current cursor position
    private float cursorBlinkTime = 0.5f;               // Time interval for blinking
    private float blinkTimer = 0f;                      // Timer for blinking
    private bool isCursorVisible = true;                // Cursor visibility state

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
            restart.SetActive(!isClipboardActive);
            pause.SetActive(!isClipboardActive);
        }

        // Handle text input if the clipboard is active
        if (isClipboardActive)
        {
            HandleCursorBlinking();
            HandleCursorMovement();
            if (Input.anyKeyDown)
            {
                HandleTextInput();
            }
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
                    textContent = textContent.Remove(cursorPosition - 1, 1);
                    cursorPosition--;
                }
            }
            else if (c == '\n' || c == '\r') // Handle Enter/Return
            {
                if (textContent.Split('\n').Length < 10)
                {
                    textContent = textContent.Insert(cursorPosition, "\n");
                    cursorPosition++;
                }
            }
            else
            {
                if (textContent.Length < 120)
                {
                    textContent = textContent.Insert(cursorPosition, c.ToString());
                    cursorPosition++;
                }
            }
        }
        UpdateTextArea();
    }
    private void HandleCursorBlinking()
    {
        blinkTimer += Time.deltaTime;
        if (blinkTimer >= cursorBlinkTime)
        {
            isCursorVisible = !isCursorVisible;
            blinkTimer = 0f;
            UpdateTextArea();
        }
    }

    private void HandleCursorMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (cursorPosition > 0)
            {
                cursorPosition--;
                UpdateTextArea();
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (cursorPosition < textContent.Length)
            {
                cursorPosition++;
                UpdateTextArea();
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveCursorVertically(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveCursorVertically(1);
        }
    }

    private void MoveCursorVertically(int direction)
    {
        string[] lines = textContent.Split('\n');
        int currentLine = 0;
        int characterInLine = 0;
        int charCount = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            if (cursorPosition <= charCount + lines[i].Length)
            {
                currentLine = i;
                characterInLine = cursorPosition - charCount;
                break;
            }
            charCount += lines[i].Length + 1; // +1 for the newline character
        }

        int newLine = Mathf.Clamp(currentLine + direction, 0, lines.Length - 1);
        int newCursorPos = 0;

        for (int i = 0; i < newLine; i++)
        {
            newCursorPos += lines[i].Length + 1; // +1 for the newline character
        }

        newCursorPos += Mathf.Clamp(characterInLine, 0, lines[newLine].Length);
        cursorPosition = Mathf.Clamp(newCursorPos, 0, textContent.Length);

        UpdateTextArea();
    }


    private void UpdateTextArea()
    {
        string displayText = textContent;

        if (isCursorVisible && isClipboardActive)
        {
            displayText = displayText.Insert(cursorPosition, "|");
        }

        textArea.text = displayText;
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
