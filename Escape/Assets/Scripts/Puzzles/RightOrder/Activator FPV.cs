using NavKeypad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActivatorFPV : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    Camera cam;

    private void Awake() => cam = Camera.main;

    // Update is called once per frame
    void Update()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            // Check if hitted a bottle
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
                {
                    keypadButton.PressButton();
                }
                if (hit.collider.TryGetComponent(out Activator activator))
                {
                    if (hit.transform.gameObject.tag == "mixer")
                    {
                        activator.SendIndex();
                    }
                    else if (hit.transform.gameObject.tag == "mixable")
                    {
                        activator.SendBottleNumber();
                    }
                }
            }
        }
        /*if (Physics.Raycast(ray, out var hitC))
        {
            if (hitC.collider.TryGetComponent(out Activator activator))
            {
                if (hitC.transform.gameObject.tag == "mixer")
                {
                    activator.InteractPossible();
                }
                else if (hitC.transform.gameObject.tag == "mixable")
                {
                    activator.InteractPossible();
                }
            }
        }*/

        if (Input.GetMouseButtonDown(1))
        {
            // Check if hitted a bottle
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.TryGetComponent(out Activator activator))
                {
                    if (hit.transform.gameObject.tag == "mixable")
                    {
                        activator.Restore();
                    }
                }
            }
        }
    }

    /*public void OnPointerEnter(PointerEventData eventData)
    {
        print("aslk");
        if (eventData.pointerEnter.gameObject.TryGetComponent(out Activator activator))
        {
            if (eventData.pointerEnter.gameObject.tag == "mixer")
            {
                activator.InteractPossible();
            }
            else if (eventData.pointerEnter.gameObject.tag == "mixable")
            {
                activator.InteractPossible();
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.TryGetComponent(out Activator activator))
        {
            if (eventData.pointerEnter.gameObject.gameObject.tag == "mixer")
            {
                activator.InteractNotPossible();
            }
            else if (eventData.pointerEnter.gameObject.gameObject.tag == "mixable")
            {
                activator.InteractNotPossible();
            }
        }
    }*/
}
