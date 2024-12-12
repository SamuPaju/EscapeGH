using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public int index;

    int limitToAdd = 2;
    [SerializeField] int currentlyAdded;
    int adding;

    [SerializeField] GameObject childLiquid;
    [SerializeField] Material[] materials;

    [SerializeField] GameObject interact;

    /// <summary>
    /// Send the index of a bottle to the Mixers script
    /// </summary>
    public void SendIndex()
    {
        Mixers.instance.SetActive(index);
    }

    /// <summary>
    /// Counts the index and sets the liquid material according to the index
    /// </summary>
    public void SendBottleNumber()
    {
        // Checks that player isn't adding colors too many times
        if (currentlyAdded < limitToAdd)
        {
            // Get the value of the mixer
            adding = Mixers.instance.Mix();
            // Checks that the same color wasn't added twice
            if (adding != index && adding != 0)
            {
                currentlyAdded++;
                index += adding;
                childLiquid.GetComponent<MeshRenderer>().material = materials[index];
                childLiquid.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    /// <summary>
    /// Restores a bottle to default
    /// </summary>
    public void Restore()
    {
        index = 0;
        currentlyAdded = 0;
        adding = 0;
        childLiquid.GetComponent<MeshRenderer>().enabled = false;
        childLiquid.GetComponent<MeshRenderer>().material = materials[index];
    }
}
