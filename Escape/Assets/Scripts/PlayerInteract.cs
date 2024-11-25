using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Instance { get; private set; }
    private Keyboard keyboard;

    private void Start()
    {
        Instance = this;
        keyboard = Keyboard.current;
    }

    private void Update()
    {
        Interact();
    }

    private void Interact()
    {
        if (keyboard.eKey.IsPressed())
        {
            IInteractable interactable = GetInteract();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public IInteractable GetInteract()
    {
        float interactRange = 1f;

        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                return interactable;
            }
        }
        return null;
    }
}