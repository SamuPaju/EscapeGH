using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    // UnityEvent to hold actions that will be triggered when the player enters the collider
    [SerializeField] public UnityEvent welcome;

    // Public property to access the UnityEvent outside the class
    public UnityEvent Welcome => welcome;

    // This method is called automatically by Unity when player enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger zone has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Invoke all actions associated with the "welcome" UnityEvent
            welcome.Invoke();
        }
    }
}
