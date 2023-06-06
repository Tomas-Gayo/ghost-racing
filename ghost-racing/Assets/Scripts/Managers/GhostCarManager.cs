using UnityEngine;
using System;

[Serializable]
public class GhostCarManager
{
    [Tooltip("Spawn position of the ghost car at the start of the race.")]
    public Transform spawnPoint;

    [HideInInspector] public GameObject instance;       // This is a reference to the current Ghost car object 

    private GhostLapPlay ghostLapPlay;                  // Reference to the ghost lap play script

    public void Setup()
    {
        // First we need to get all the references of the components
        ghostLapPlay = instance.GetComponent<GhostLapPlay>();
    }

    public void EnableControl()
    {
        ghostLapPlay.enabled = true;
    }

    public void DisableContol()
    {
        ghostLapPlay.enabled = false;
    }

    public void Reset()
    {
        instance.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
    }
}
