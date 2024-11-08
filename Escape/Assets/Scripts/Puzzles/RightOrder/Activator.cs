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


    /// <summary>
    /// Send the index of a bottle to the Mixers script
    /// </summary>
    public void SendIndex()
    {
        Mixers.instance.SetActive(index);
    }

    // Count the index
    public void SendBottleNumber()
    {
        if (currentlyAdded < limitToAdd)
        {
            adding = Mixers.instance.Mix();
            if (adding != index)
            {
                currentlyAdded++;
                index += adding;
                childLiquid.GetComponent<MeshRenderer>().material = materials[index];
                childLiquid.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    // Restores a bottle to default
    public void Restore()
    {
        index = 0;
        currentlyAdded = 0;
        adding = 0;
        childLiquid.GetComponent<MeshRenderer>().enabled = false;
        childLiquid.GetComponent<MeshRenderer>().material = materials[index];
    }
}
