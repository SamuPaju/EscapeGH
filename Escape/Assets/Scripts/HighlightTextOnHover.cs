using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class HighlightTextOnHoverTMP : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Text component
    public TMP_Text text;
    // Font size when the text is in its normal state
    public float normalSize = 40;
    // Font size when the text is highlighted
    public float highlightSize = 45f;

    private void Start()
    {
        // If the text component is not manually assigned, try to find it in child objects
        if (text == null)
        {
            text = GetComponentInChildren<TMP_Text>();
        }

        // Set the initial font size to the normal size if the text component is found
        if (text != null)
        {
            text.fontSize = normalSize;
        }
    }

    // Triggered when the mouse pointer enters the area of the text
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the font size to the highlighted size if the text component is available
        if (text != null)
        {
            text.fontSize = highlightSize;
        }
    }

    // Triggered when the mouse pointer exits the area of the text
    public void OnPointerExit(PointerEventData eventData)
    {
        // Revert the font size to the normal size if the text component is available
        if (text != null)
        {
            text.fontSize = normalSize;
        }
    }
}
