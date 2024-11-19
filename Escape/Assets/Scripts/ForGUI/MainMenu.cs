using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject mainMenu;
    public GameObject loadingScreen;
    //if press start botton, load game scene
    public void StartGame()
    {
        loadingScreen.SetActive(true);
        FindObjectOfType<Loading>().LoadScene("test");
    }

    //if press settings botton, open settings
    public void Settings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    //if press back botton, close settings and open main menu
    public void Back()
    {
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // for exit game in unity
        #else
            Application.Quit(); // exit game
        #endif
    }
}
