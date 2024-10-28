using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusMode : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] GameObject focusCamera;
    public bool inArea;

    [SerializeField] float offsetX = 0f;
    [SerializeField] float offsetY = 0f;
    [SerializeField] float offsetZ = 0f;
    
    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inArea)
        {
            playerMovement.SwitchCam();

            focusCamera.transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z + offsetZ);
        }
    }
}
