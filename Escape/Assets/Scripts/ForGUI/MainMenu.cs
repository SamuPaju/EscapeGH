using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // UI elements for settings, main menu, and loading screen
    public GameObject settings;
    public GameObject mainMenu;
    public GameObject loadingScreen;

    // Method to start the game when the "Start" button is pressed
    public void StartGame()
    {
        // Activate the loading screen
        loadingScreen.SetActive(true);

        // Use the Loading script to load the "test" scene 
        FindObjectOfType<Loading>().LoadScene("test");
    }

    // Method to open the settings menu when the "Settings" button is pressed
    public void Settings()
    {
        // Show the settings menu and hide the main menu
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    // Method to return to the main menu when the "Back" button is pressed
    public void Back()
    {
        // Hide the settings menu and show the main menu
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    // Method to exit the game when the "Exit" button is pressed
    public void ExitGame()
    {
#if UNITY_EDITOR
        // If running in the Unity Editor, stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // If running as a built application, quit the game
            Application.Quit(); 
#endif
    }
}
