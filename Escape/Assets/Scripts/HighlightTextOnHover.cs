using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class HighlightTextOnHoverTMP : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text text; 
    public float normalSize = 40; 
    public float highlightSize = 45f; 

    private void Start()
    {
        if (text == null)
        {
            text = GetComponentInChildren<TMP_Text>();
        }

        
        if (text != null)
        {
            text.fontSize = normalSize;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (text != null)
        {
            text.fontSize = highlightSize;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (text != null)
        {
            text.fontSize = normalSize;
        }
    }
}
