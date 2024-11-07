using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixers : MonoBehaviour
{

    public static Mixers instance;

    bool blueActive;
    bool yellowActive;
    bool redActive;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;   
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
        else if (index == 2)
        {
            yellowActive = true;
        }
        else if (index == 4)
        {
            redActive = true;
        }
    }

    public int Mix(int bottleNumber)
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
        print("Jup seems to work");
        blueActive = false;
        yellowActive = false;
        redActive = false;
    }
}
