using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DisplayAnimatedText : MonoBehaviour
{
    // TextMeshProUGUI component where the text will be displayed
    public TextMeshProUGUI textDisplay;
    // Array of messages to display one by one
    public string[] messages;
    // Speed at which letters appear in the animated text
    public float typingSpeed = 0.05f;
    // Delay before clearing the text after all messages are displayed
    public float delayBeforeClear = 1f;
    // Tracks whether the text sequence has started
    private bool isTextStarted = false;
    // Tracks whether text is currently being typed out
    private bool isTyping = false;

    // UnityEvent triggered after the text sequence is complete
    [SerializeField] private UnityEvent startText;
    public UnityEvent StartText => startText;

    // Triggered when a collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player and the text sequence hasn't started yet
        if (other.CompareTag("Player") && !isTextStarted)
        {
            isTextStarted = true;
            StartCoroutine(PlayTextSequence());
        }
    }

    // Coroutine to play all messages in sequence
    private IEnumerator PlayTextSequence()
    {
        // Loop through each message in the messages array
        foreach (string message in messages)
        {
            // Animate the current message letter by letter
            yield return StartCoroutine(AnimateText(message));
            // Wait for 1 second before showing the next message
            yield return new WaitForSeconds(1f);
        }

        // Wait for a delay before clearing the text
        yield return new WaitForSeconds(delayBeforeClear);

        // Clear the text from the screen
        textDisplay.text = "";

        // Invoke the UnityEvent to signal the end of the text sequence
        startText.Invoke();
    }

    // Coroutine to animate a single message letter by letter
    private IEnumerator AnimateText(string message)
    {
        isTyping = true; // Mark that text is currently being typed
        textDisplay.text = ""; // Clear any existing text before starting

        // Add each letter of the message to the text display
        foreach (char letter in message)
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Wait between letters
        }

        isTyping = false; // Mark that typing is complete
    }
}
