using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private TMP_Text loadingText; // Downloading
    [SerializeField] private string baseMessage = "Downloading";

    private void Start()
    {
        StartCoroutine(AnimateDots());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator AnimateDots()
    {
        int dotCount = 0;
        while (true)
        {
            // Dots animation
            loadingText.text = baseMessage + new string('.', dotCount);
            dotCount = (dotCount + 1) % 4; //Only 3 dots
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        // While scene donloading this downlpoading canvas goes
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                // When downloading almost done switch scenes
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
