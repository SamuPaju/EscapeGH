using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //if press start botton, load game scene
    public void StartGame()
    {
        SceneManager.LoadScene("test");
    }

    //if press settings botton, load settings scene
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    //if press back botton, load main menu
    public void Back()
    {
        SceneManager.LoadScene("StartScreen");
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
