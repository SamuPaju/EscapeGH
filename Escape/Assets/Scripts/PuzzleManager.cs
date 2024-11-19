using NavKeypad;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private RightOrder rightOrder;
    [SerializeField] private Keypad keypad;
    [SerializeField] private PuzzleFour puzzleFour;
    [SerializeField] private Transform player; 

    private void Start()
    {
        LoadPlayerPosition();
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
        if (rightOrder != null && rightOrder.puzzleIsDone)
        {
            SavePlayerPosition();
        }
    }

    private void SecondPuzzle()
    {
       
    }

    private void ThirdPuzzle()
    {
      
    }

    private void FourPuzzle()
    {
        if (puzzleFour != null && puzzleFour.gameCompleted == true)
        {
          
        }
    }

    private void SavePlayerPosition()
    {
        if (player != null)
        {
            PlayerPrefs.SetFloat("PlayerPosX", player.position.x);
            PlayerPrefs.SetFloat("PlayerPosY", player.position.y);
            PlayerPrefs.SetFloat("PlayerPosZ", player.position.z);

            PlayerPrefs.Save(); 
            Debug.Log("Player position saved.");
        }
    }

    private void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");

            player.position = new Vector3(x, y, z);
            Debug.Log("Player position loaded.");
        }
        else
        {
            Debug.Log("No saved player position found.");
        }
    }

    private void DeleteAllSaves()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); 
        Debug.Log("All saves deleted.");
    }

}
