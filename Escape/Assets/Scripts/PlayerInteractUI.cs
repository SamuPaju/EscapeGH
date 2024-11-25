using TMPro;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private TMP_Text actionText;

    private void Update()
    {
        if (PlayerInteract.Instance.GetInteract() != null)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        container.SetActive(true);
        actionText.text = PlayerInteract.Instance.GetInteract().GetInteractText();
    }

    void Hide()
    {
        container?.SetActive(false);
    }
}