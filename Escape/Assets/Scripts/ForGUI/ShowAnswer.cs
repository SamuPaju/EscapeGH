using UnityEngine;

public class ShowAnswer : MonoBehaviour
{
    // Button GameObject that will trigger the answer
    public GameObject AnswerButton;

    // GameObject containing the answer
    public GameObject Answer;

    // Method to show the answer and hide the button
    public void ShowAnswers()
    {
        // Check if the AnswerButton is assigned
        if (AnswerButton != null)
        {
            // Hide the button by deactivating it
            AnswerButton.SetActive(false);

            // Show the answer by activating it
            Answer.SetActive(true);
        }
    }
}
