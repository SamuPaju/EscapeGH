using NavKeypad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorFPV : MonoBehaviour
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
}
