using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixers : MonoBehaviour
{
    public static Mixers instance;

    bool blueActive;
    bool yellowActive;
    bool redActive;

    public GameObject activeBlue; 
    public GameObject activeYellow; 
    public GameObject activeRed;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;   
    }

    public void SetActive(int index)
    {
        // If one of the mixers are active set all of them to false
        if (blueActive || yellowActive || redActive)
        {
            AllDefault();
        }

        // Set the correct bottle active
        if (index == 1)
        {
            blueActive = true;
            activeBlue.SetActive(true);
        }
        else if (index == 2)
        {
            yellowActive = true;
            activeYellow.SetActive(true);
        }
        else if (index == 4)
        {
            redActive = true;
            activeRed.SetActive(true);
        }
    }

    public int Mix()
    {
        if (blueActive || yellowActive || redActive)
        {
            if (blueActive)
            {
                AllDefault();
                return 1;
            }
            if (yellowActive)
            {
                AllDefault();
                return 2;
            }
            else
            {
                AllDefault();
                return 4;
            }
        }
        return 0;
    }

    void AllDefault()
    {
        blueActive = false;
        activeBlue.SetActive(false);
        yellowActive = false;
        activeYellow.SetActive(false);
        redActive = false;
        activeRed.SetActive(false);
    }
}
