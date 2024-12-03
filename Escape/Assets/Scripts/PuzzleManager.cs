using NavKeypad;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // References to puzzle components and UI elements
    [SerializeField] private RightOrder rightOrder;
    [SerializeField] private Keypad keypad;
    [SerializeField] private EndMarble endMarble;
    [SerializeField] private PuzzleFour puzzleFour;
    [SerializeField] private Transform player;
    [SerializeField] private StartGame startGame;
    [SerializeField] private TextMeshProUGUI saveNotificationText;

    private string saveFilePath; // Path to the save file

    private void Start()
    {
        // Initialize the save file path
        Debug.Log($"Save file path: {Application.persistentDataPath}/saveData.json");
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");

        // Load saved player position and puzzle states on startup
        //LoadPlayerPosition();

        // Delete all saves (for debugging or testing purposes)
       DeleteAllSaves();
    }

    private void Update()
    {
        // Check and handle each puzzle's completion in the update loop
        FirstPuzzle();
        SecondPuzzle();
        ThirdPuzzle();
        FourPuzzle();
    }

    // Handle logic for the first puzzle
    private void FirstPuzzle()
    {
        if (rightOrder != null)
        {
            // Save progress if the puzzle is done and the event hasn't been triggered yet
            if (rightOrder.puzzleIsDone && !HasWelcomeEventBeenTriggered())
            {
                SaveWelcomeEventState();
                rightOrder.correctOrder?.Invoke();
                SaveGrantedEventState();
                SavePlayerPosition();
                ShowSaveNotification();
            }
        }
    }

    // Handle logic for the second puzzle
    private void SecondPuzzle()
    {
        if (keypad.accessWasGranted == true)
        {
            SavePlayerPosition();
            SaveAccessGrantedEventState();
            ShowSaveNotification();
            keypad.accessWasGranted = false;
        }
    }

    // Handle logic for the third puzzle
    private void ThirdPuzzle()
    {
        if (endMarble.puzzleDone == true)
        {
            SavePlayerPosition();
            ShowSaveNotification();
            endMarble.puzzleDone = false;
        }
    }

    // Handle logic for the fourth puzzle
    private void FourPuzzle()
    {
        if (puzzleFour != null && puzzleFour.gameCompleted == true)
        {
            SavePlayerPosition();
            Done();
            ShowSaveNotification();
        }
    }

    /// <summary>
    /// Save the player's position
    /// </summary>
    private void SavePlayerPosition()
    {
        if (player != null)
        {
            Debug.Log("save position");
            SaveData saveData = LoadSaveData();
            saveData.playerPosition = new Vector3Data(player.position);

            SaveToFile(saveData);
        }
    }

    // Show a save notification for 2 seconds
    private void ShowSaveNotification()
    {
        if (saveNotificationText != null)
        {
            Debug.Log("yesyes");
            StartCoroutine(ShowSaveNotificationCoroutine());
        }
    }

    private IEnumerator ShowSaveNotificationCoroutine()
    {
        // Display the save notification
        saveNotificationText.gameObject.SetActive(true);

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Hide the save notification
        saveNotificationText.gameObject.SetActive(false);
    }

    // Save the state of the welcome event
    private void SaveWelcomeEventState()
    {
        SaveData saveData = LoadSaveData();

        if (!saveData.isWelcomeEventTriggered)
        {
            saveData.isWelcomeEventTriggered = true;
            SaveToFile(saveData);
            Debug.Log("Welcome event state saved.");
        }
    }

    // Save the state of the granted event (after 1 puzzle)
    private void SaveGrantedEventState()
    {
        SaveData saveData = LoadSaveData();
        saveData.isGrantedTriggered = true;

        SaveToFile(saveData);
        Debug.Log("Access granted event state saved.");
    }

    // Save the state of the access granted event (after 2 puzzle)
    private void SaveAccessGrantedEventState()
    {
        SaveData saveData = LoadSaveData();
        saveData.isAccessGrantedTriggered = true;

        SaveToFile(saveData);
        Debug.Log("Access granted event state saved.");
    }

    // Save the state of the fourth puzzle completion
    private void Done()
    {
        SaveData saveData = LoadSaveData();
        saveData.Done = true;

        SaveToFile(saveData);
        Debug.Log("Last save saved");
    }

    // Load the player's saved position and other puzzle states
    private void LoadPlayerPosition()
    {
        if (File.Exists(saveFilePath))
        {
            SaveData saveData = LoadSaveData();

            if (saveData.playerPosition != null)
            {
                player.position = saveData.playerPosition.ToVector3();
                Debug.Log("Player position loaded from JSON.");
            }

            if (saveData.isWelcomeEventTriggered && startGame?.welcome != null)
            {
                startGame.welcome.Invoke();
                Debug.Log("Welcome event re-triggered.");
            }

            if (saveData.isGrantedTriggered && rightOrder?.correctOrder != null)
            {
                rightOrder.correctOrder.Invoke();
                Debug.Log("Access granted event re-triggered.");
            }

            if (saveData.isAccessGrantedTriggered && keypad?.onAccessGranted != null)
            {
                keypad.onAccessGranted.Invoke();
                Debug.Log("Access granted event re-triggered.");
            }

            if (saveData.Done && puzzleFour?.done != null)
            {
                puzzleFour.done.Invoke();
                Debug.Log("Done.");
            }
        }
        else
        {
            Debug.Log("No saved player position found.");
        }
    }

    // Check if the welcome event has already been triggered
    private bool HasWelcomeEventBeenTriggered()
    {
        SaveData saveData = LoadSaveData();
        return saveData.isWelcomeEventTriggered;
    }

    // Delete all saved data (for debugging or testing purposes)
    private void DeleteAllSaves()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted.");
        }
    }

    // Load saved data from file or return a new SaveData object if the file doesn't exist
    private SaveData LoadSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<SaveData>(jsonData);
        }

        return new SaveData();
    }

    // Save data to file
    private void SaveToFile(SaveData saveData)
    {
        string jsonData = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, jsonData);
    }

    // Classes for saving data
    [System.Serializable]
    public class SaveData
    {
        public Vector3Data playerPosition;
        public bool isWelcomeEventTriggered;
        public bool isGrantedTriggered;
        public bool isAccessGrantedTriggered;
        public bool Done;
    }

    [System.Serializable]
    public class Vector3Data
    {
        public float x;
        public float y;
        public float z;

        public Vector3Data(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}
