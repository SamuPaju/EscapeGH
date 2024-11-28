using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class DisplayAnimatedText : MonoBehaviour
{
    public TextMeshProUGUI textDisplay; // TextMesh Pro
    public string[] messages; // All lines
    public float typingSpeed = 0.05f; // Animation speed
    public float delayBeforeClear = 1f; // Time before clean

    private bool isTextStarted = false; // Was text used??
    private bool isTyping = false;

    [SerializeField] private UnityEvent startText;

    public UnityEvent StartText => startText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTextStarted) // Player tag and text status
        {
            isTextStarted = true; // Start text
            StartCoroutine(PlayTextSequence());
        }
    }

    private IEnumerator PlayTextSequence()
    {
        foreach (string message in messages)
        {
            yield return StartCoroutine(AnimateText(message)); // Every lines go after each other
            yield return new WaitForSeconds(1f); // Wait
        }

        yield return new WaitForSeconds(delayBeforeClear); // Wait before clean text
        textDisplay.text = ""; // Delete text from the screen
        startText.Invoke();
    }

    private IEnumerator AnimateText(string message)
    {
        isTyping = true;
        textDisplay.text = ""; // Delete text before start
        foreach (char letter in message)
        {
            textDisplay.text += letter; // Goes letter by letter
            yield return new WaitForSeconds(typingSpeed); // Wait betwen letters
        }
        isTyping = false;
    }
}
