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

    /// <summary>
    /// Activates the selected mixer
    /// </summary>
    /// <param name="index"></param>
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

    /// <summary>
    /// Checks what should be returned when the player hits a mixable
    /// </summary>
    /// <returns></returns>
    public int Mix()
    {
        // Check if any of the mixers are active
        if (blueActive || yellowActive || redActive)
        {
            // Set mixers to default and return the active ones corresponding number
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

    /// <summary>
    /// Set all mixers to default
    /// </summary>
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
