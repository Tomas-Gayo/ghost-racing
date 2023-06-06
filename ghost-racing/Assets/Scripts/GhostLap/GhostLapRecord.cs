using UnityEngine;

public class GhostLapRecord : MonoBehaviour
{
    [Tooltip("Time will pass between samples.")]
    public float timeBetweenSamples = 0.25f;
    [Tooltip("Referece to the scriptable object where the data of the lap will be saved.")]
    public GhostLapData bestLapSO;                      
    
    private Transform carToRecord;                      // Reference to the car that is going to be recorded.
    private float currentTimeBetweenSamples = 0.0f;     // Current time so it is possible to take samples in a certain time.

    private void Awake()
    {
        carToRecord = GetComponent<Transform>();
    }

    private void Update()
    {
        // Increment time on each frame 
        currentTimeBetweenSamples += Time.deltaTime;

        // If current time is greater than the sample time ...
        if (currentTimeBetweenSamples >= timeBetweenSamples)
        {
            // Save info of the car
            bestLapSO.AddNewData(carToRecord.transform);
            // and reset current time
            currentTimeBetweenSamples -= timeBetweenSamples;        
        }
    }

}
