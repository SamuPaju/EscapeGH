using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationActivator : MonoBehaviour
{
    public static StationActivator instance;

    [SerializeField] GameObject player;

    [Header("Station 1 variables")]
    [SerializeField] GameObject MCDec;
    [SerializeField] GameObject MCFunc;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Activates a station (Call comes from player)
    /// </summary>
    public void ActivateStation(int stationIndex)
    {
        player.SetActive(false);

        if (stationIndex == 1)
        {
            MCDec.SetActive(false);
            MCFunc.SetActive(true);
        }
    }

    /// <summary>
    /// Quits a station (Call comes from scripts running the station)
    /// </summary>
    public void QuitStation(int stationIndex)
    {
        player.SetActive(true);

        if (stationIndex == 1)
        {
            MCDec.SetActive(true);
            MCFunc.SetActive(false);
        }
    }
}
