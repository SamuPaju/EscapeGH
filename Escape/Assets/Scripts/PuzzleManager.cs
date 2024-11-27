using UnityEngine;
using UnityEngine.Events;
using System.IO;
using NavKeypad;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private RightOrder rightOrder;
    [SerializeField] private Keypad keypad;
    [SerializeField] private EndMarble endMarble;
    [SerializeField] private PuzzleFour puzzleFour;
    [SerializeField] private Transform player;
    [SerializeField] private StartGame startGame;

    private string saveFilePath;

    private void Start()
    {
        Debug.Log($"Save file path: {Application.persistentDataPath}/saveData.json");
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");

        // Load the player's position save on startup
        LoadPlayerPosition();

        // Delete saves if needed (temporarily)
        //DeleteAllSaves();
    }

    private void Update()
    {
        FirstPuzzle();
        SecondPuzzle();
        ThirdPuzzle();
        FourPuzzle();
    }

    private void FirstPuzzle()
    {
        if (rightOrder != null)
        {
            // If the first puzzle is completed, save the welcome event
            if (rightOrder.puzzleIsDone && !HasWelcomeEventBeenTriggered())
            {
                SaveWelcomeEventState();
                rightOrder.correctOrder?.Invoke();
                SaveAccessGrantedEventState();
                SavePlayerPosition();
            }
        }
    }

    private void SecondPuzzle()
    {
        if (keypad.accessWasGranted == true)
        {
            SavePlayerPosition();
        }
    }

    private void ThirdPuzzle()
    {
        if (endMarble.puzzleDone == true)
        {
            SavePlayerPosition();
        }
    }

    private void FourPuzzle()
    {
        if (puzzleFour != null && puzzleFour.gameCompleted == true)
        {
            SavePlayerPosition();
        }
    }

    private void SavePlayerPosition()
    {
        if (player != null)
        {
            // Create an object to store data
            SaveData saveData = LoadSaveData();
            saveData.playerPosition = new Vector3Data(player.position);

            // Save JSON to a file
            SaveToFile(saveData);
            Debug.Log("Player position saved to JSON.");
        }
    }

    private void SaveWelcomeEventState()
    {
        SaveData saveData = LoadSaveData();

        //Save state only if the event has not been saved yet
        if (!saveData.isWelcomeEventTriggered)
        {
            saveData.isWelcomeEventTriggered = true;
            SaveToFile(saveData);
            Debug.Log("Welcome event state saved.");
        }
    }

    private void SaveAccessGrantedEventState()
    {
        SaveData saveData = LoadSaveData();
        saveData.isAccessGrantedTriggered = true;

        SaveToFile(saveData);
        Debug.Log("Access granted event state saved.");
    }

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

            //Check if the Welcome event has already been called
            if (saveData.isWelcomeEventTriggered && startGame?.welcome != null)
            {
                startGame.welcome.Invoke();
                Debug.Log("Welcome event re-triggered.");
            }

            if (saveData.isAccessGrantedTriggered && rightOrder?.correctOrder != null)
            {
                rightOrder.correctOrder.Invoke();
                Debug.Log("Access granted event re-triggered.");
            }
        }
        else
        {
            Debug.Log("No saved player position found.");
        }
    }

    private bool HasWelcomeEventBeenTriggered()
    {
        SaveData saveData = LoadSaveData();
        return saveData.isWelcomeEventTriggered;
    }

    private void DeleteAllSaves()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted.");
        }
    }

    private SaveData LoadSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<SaveData>(jsonData);
        }

        return new SaveData();
    }

    private void SaveToFile(SaveData saveData)
    {
        string jsonData = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, jsonData);
    }

    // Classes for storing data
    [System.Serializable]
    public class SaveData
    {
        public Vector3Data playerPosition;
        public bool isWelcomeEventTriggered;
        public bool isAccessGrantedTriggered;
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
