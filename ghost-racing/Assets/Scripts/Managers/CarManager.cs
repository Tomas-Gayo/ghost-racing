using UnityEngine;
using System;
using UnityStandardAssets.Vehicles.Car;

[Serializable]
public class CarManager
{
    [Tooltip("Spawn position of the car at the start of the race.")]
    public Transform spawnPoint;

    [HideInInspector] public GameObject instance;       // This is a reference to the current car object instantiated 

    private CarUserControl userControl;                 // Reference to the car controller script that allows the movement.
    private GhostLapRecord ghostRecord;                 // Reference to the ghost car record controller

    public void Setup()
    {
        // First we need to get all the references of the components
        userControl = instance.GetComponent<CarUserControl>();
        ghostRecord = instance.GetComponent<GhostLapRecord>();
    }

    public void EnableControl()
    {
        userControl.enabled = true;
        ghostRecord.enabled = true;
    }

    public void DisableContol()
    {
        userControl.enabled = false;
        ghostRecord.enabled = false;
    }
}
