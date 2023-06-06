using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GhostLapData", order = 1)]
public class GhostLapData : ScriptableObject
{
    // Current lap positon and rotation
    [HideInInspector] public List<Vector3> carPositions;
    [HideInInspector] public List<Quaternion> carRotations;

    // Best lap position and rotation
    public List<Vector3> carBestPositions;
    public List<Quaternion> carBestRotations;

    // Best time
    public float bestTime;

    // Set the current positions and rotations
    public void AddNewData(Transform transform)
    {
        carPositions.Add(transform.position);
        carRotations.Add(transform.rotation);
    }

    // Get best position and rotation given a sample
    public void GetDataAt(int sample, out Vector3 position, out Quaternion rotation)
    {
        position = carBestPositions[sample];
        rotation = carBestRotations[sample];
    }

    // To avoid the out of index in the play script so there are no error reading the list with more samples that the current ones
    public int GetIndexSample()
    {
        return carBestPositions.Count;
    }

    // Compares the current time with the best time ...
    public void SetBestTime(float currentTime)
    {   
        // ... if it gets a better time ...
        if (currentTime < bestTime)
        {
            // best time, position and rotation are saved
            bestTime = currentTime;
            AddNewBestData();
            // Reset current list
            Reset();
        }
    }

    // Used to know if there is a best time or not 
    public bool IsBestData()
    {
        if (carBestPositions.Count > 0)
            return true;
        else
            return false;
    }

    // Clear current race positions and rotations
    public void Reset()
    {
        carPositions.Clear();
        carRotations.Clear();
    }
    // Copy all the current lap data lists on the best time lists 
    private void AddNewBestData()
    {
        carBestPositions = new List<Vector3>(carPositions);
        carBestRotations = new List<Quaternion>(carRotations);
    }

    // Clear current race positions and rotations always we close the game
    private void OnDisable()
    {
        carPositions.Clear();
        carRotations.Clear();
    }
}
