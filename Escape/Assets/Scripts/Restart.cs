using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public static Restart instance;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        // Calls RestartGame() if M is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            RestartGame();
        }
    }

    /// <summary>
    /// If player hits this objects BoxCollider call RestartGame()
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RestartGame();
        }
    }

    /// <summary>
    /// Restart the hole game
    /// </summary>
    public void RestartGame()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}
