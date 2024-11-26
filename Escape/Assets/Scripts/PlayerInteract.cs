using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public GameObject hintText; // Hint text
    public Transform hintPosition; // Text position
    private Transform player; // Player

    void Start()
    {
        // Player need to have tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (hintText != null)
        {
            hintText.SetActive(false);
        }
    }

    void Update()
    {
        if (hintText != null && player != null)
        {
            float distance = Vector3.Distance(player.position, transform.position);

            // Turn on text
            if (distance < 2f) // Distance to turn on text
            {
                hintText.SetActive(true);

                // Put text next to object
                Vector3 screenPos = Camera.main.WorldToScreenPoint(hintPosition.position);
                hintText.transform.position = screenPos;
            }
            else
            {
                hintText.SetActive(false);
            }
        }
    }
}