using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationActivator : MonoBehaviour
{
    public static StationActivator instance;

    [SerializeField] GameObject player;
    [SerializeField] GameObject centralDot;

    [Header("Station 1 variables")]
    [SerializeField] GameObject MCDec;
    [SerializeField] GameObject MCFunc;
    [SerializeField] GameObject controls;
    bool completed = true;

    void Start()
    {
        instance = this;
    }

    /// <summary>
    /// Activates a station (Call comes from player)
    /// </summary>
    public void ActivateStation(int stationIndex)
    {
        // Checks which station needs to be activated
        if (stationIndex == 1 && completed)
        {
            player.SetActive(false);
            //centralDot.SetActive(false);

            MCDec.SetActive(false);
            MCFunc.SetActive(true);
            controls.SetActive(true);
        }

        else
        {
            Debug.Log("Sorry but there insn't a station with index: " +  stationIndex);
        }
    }

    /// <summary>
    /// Quits a station (Call comes from scripts running the station)
    /// </summary>
    public void QuitStation(int stationIndex)
    {
        player.SetActive(true);
        //centralDot.SetActive(true);

        // Checks which station needs to be deactivated
        if (stationIndex == 1) // Station 1 Quit
        {
            MCDec.SetActive(true);
            MCFunc.SetActive(false);
            controls.SetActive(false);
        }
        else if (stationIndex == 2) // Station 1 Win
        {
            MCDec.SetActive(true);
            MCFunc.SetActive(false);
            controls.SetActive(false);
            completed = false;
        }

        else
        {
            Debug.Log("Sorry but there insn't a station with index: " + stationIndex);
        }
    }
}
