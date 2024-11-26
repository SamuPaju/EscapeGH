using NavKeypad;
using UnityEngine;
using System.IO;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private RightOrder rightOrder;
    [SerializeField] private Keypad keypad;
    [SerializeField] private EndMarble endMarble;
    [SerializeField] private PuzzleFour puzzleFour;
    [SerializeField] private Transform player;

    private string saveFilePath;

    private void Start()
    {
        Debug.Log($"Save file path: {Application.persistentDataPath}/saveData.json");
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");

        // Загружаем сохранение позиции игрока при запуске
        //LoadPlayerPosition();

        // Удалить сохранения, если нужно (временно)
        DeleteAllSaves();
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
        if (rightOrder != null && rightOrder.puzzleIsDone)
        {
            SavePlayerPosition();
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
            // Создаем объект для хранения данных
            SaveData saveData = new SaveData
            {
                playerPosition = new Vector3Data(player.position)
            };

            // Конвертируем данные в JSON
            string jsonData = JsonUtility.ToJson(saveData, true);

            // Сохраняем JSON в файл
            File.WriteAllText(saveFilePath, jsonData);

            Debug.Log("Player position saved to JSON.");
        }
    }

    private void LoadPlayerPosition()
    {
        if (File.Exists(saveFilePath))
        {
            // Читаем JSON из файла
            string jsonData = File.ReadAllText(saveFilePath);

            // Конвертируем JSON обратно в объект
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);

            // Восстанавливаем позицию игрока
            player.position = saveData.playerPosition.ToVector3();

            Debug.Log("Player position loaded from JSON.");
        }
        else
        {
            Debug.Log("No saved player position found.");
        }
    }

    private void DeleteAllSaves()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted.");
        }
    }

    // Классы для хранения данных
    [System.Serializable]
    public class SaveData
    {
        public Vector3Data playerPosition;
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
