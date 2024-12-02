using NavKeypad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorFPV : MonoBehaviour
{
    Camera cam;

    private void Awake() => cam = Camera.main;

    void Update()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            // Does the Raycast
            if (Physics.Raycast(ray, out var hit))
            {
                // Checks if the Raycast hitted a keypad or bottles
                if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
                {
                    keypadButton.PressButton();
                }
                if (hit.collider.TryGetComponent(out Activator activator))
                {
                    // Checks which bottles were hit
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

        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            // Does the Raycast
            if (Physics.Raycast(ray, out var hit))
            {
                // Gets the hitted coomponents Activator script
                if (hit.collider.TryGetComponent(out Activator activator))
                {
                    // Check if hitted a mixable
                    if (hit.transform.gameObject.tag == "mixable")
                    {
                        activator.Restore();
                    }
                }
            }
        }
    }
}
