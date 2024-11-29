using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Text element to display the loading message and animation
    [SerializeField] private TMP_Text loadingText;
    // Base message displayed before the dots animation
    [SerializeField] private string baseMessage = "Downloading";

    private void Start()
    {
        // Start the coroutine to animate the dots in the loading text
        StartCoroutine(AnimateDots());
    }

    // Public method to start loading a new scene
    public void LoadScene(string sceneName)
    {
        // Start the coroutine to load the scene
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // Coroutine to animate dots appearing after the base message
    private IEnumerator AnimateDots()
    {
        int dotCount = 0; // Keeps track of the number of dots
        while (true)
        {
            // Update the loading text with the current number of dots
            loadingText.text = baseMessage + new string('.', dotCount);
            dotCount = (dotCount + 1) % 4; // Loop dot count from 0 to 3 (max 3 dots)
            yield return new WaitForSeconds(0.5f); // Wait 0.5 seconds before updating again
        }
    }

    // Coroutine to load a scene
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Begin loading the scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        // Prevent the scene from activating immediately
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Check if the loading progress is almost complete (90% or more)
            if (operation.progress >= 0.9f)
            {
                // Allow the scene to activate once loading is done
                operation.allowSceneActivation = true;
            }
            yield return null; // Wait for the next frame before continuing the loop
        }
    }
}
