using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private UnityEvent firstEnd;
    [SerializeField] private UnityEvent end;
    [SerializeField] private Image whiteScreen; // White screen
    [SerializeField] private Image blackScreen; // Black screen
    [SerializeField] private TextMeshProUGUI endText; // Text "The End"
    [SerializeField] private TextMeshProUGUI questionText; // Text "???"

    public UnityEvent FirstEnd => firstEnd;
    public UnityEvent End => end;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            firstEnd.Invoke();
            StartCoroutine(EndActivate());
        }
    }

    private IEnumerator EndActivate()
    {
        yield return new WaitForSeconds(4f);
        end.Invoke();

        // Fade in white screen
        yield return StartCoroutine(FadeIn(whiteScreen, 1f));

        // Delay then switch to black screen
        yield return new WaitForSeconds(1f);
        whiteScreen.gameObject.SetActive(false);
        blackScreen.gameObject.SetActive(true);
        endText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        // Adding "???"
        questionText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);

        // Close the game
        QuitGame();
    }

    private IEnumerator FadeIn(Image image, float duration)
    {
        Color color = image.color;
        color.a = 0f; // Initial transparency
        image.color = color;
        image.gameObject.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / duration); // Linear increase in alpha
            image.color = color;
            yield return null;
        }

        color.a = 1f; // Setting the final transparency
        image.color = color;
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        // Close the game in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Закрыть игру в сборке
        Application.Quit();
#endif
    }
}
