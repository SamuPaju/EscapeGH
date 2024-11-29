using UnityEngine;

public class BoxUp : MonoBehaviour
{
    // Animator component used to control box animations
    [SerializeField] private Animator anim;

    // Public property to check if the box is currently in the "up" state
    public bool Up => up;

    // Private variable to track the state of the box (up or not)
    private bool up = false;

    // Method to make the box move up
    public void GoesUp()
    {
        up = true; // Set the "up" state to true
        anim.SetBool("GoesUp", up); // Trigger the "GoesUp" animation in the Animator
    }
}
