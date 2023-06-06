using System.Collections.Generic;
using UnityEngine;

public class TimeManager: MonoBehaviour
{
    [Tooltip("Referece to the scriptable object where the data of the lap will be saved.")]
    public GhostLapData bestLapSO;

    [HideInInspector] public List<float> times = new List<float>();              // List used to save all the lap times
    [HideInInspector] public float time;                                         // Current time of the game
    [HideInInspector] public bool isBestTime;                                    // Return true if there was a new best time false if not

    private string currentTimeText;                                              // Current time of the lap in a string
    private string bestTimeText;                                                 // best time of all the list of times in a string
    private bool isRunning = false;                                              // Boolean to stop the time when race is finish
    private float lapTime;                                                        // This is the time of each lap
    private float previousTime;                                                   // Each time the car goes through the finish line the current total time is saved

    private void Start()
    {
        previousTime = 0;
    }

    void Update()
    {
        if (!isRunning)
            time += Time.deltaTime;
    }

    // Add the time in a list after trasspassing the finish line
    public void SetTime()
    {
        lapTime = time - previousTime;
        times.Add(lapTime);
    }

    // This will save the time after going through the finish line so it is possible to calculate the individual lap times
    public void SetPreviousTime()
    {
        previousTime = time;
    }

    // Returns the current time as string
    public string GetCurrentTimeString()
    {
        currentTimeText = ConvertTimeToString(time); ;
        return currentTimeText;
    }

    // Returns the lap time needed as string
    public string GetTimeString(int lap)
    {
        if (times.Count < 0) return null;

        return ConvertTimeToString(times[lap]);
    }
    
    // Returns the best time as string
    public string GetBestTimeString()
    {
        bestTimeText = ConvertTimeToString(bestLapSO.bestTime);
        return bestTimeText;
    }

    // This is used to compare all the values on the time list so we can get the best time
    public void SetFinalTime()
    {
        bestLapSO.SetBestTime(time);
    }
 
    // Stop time
    public void Stop()
    {
        isRunning = true;
    }

    // We need to display the time as a string so we can displayed on the screen in a pretty format
    private string ConvertTimeToString(float time)
    {
        int timeInt = (int)(time);
        int minutes = timeInt / 60;
        int seconds = timeInt % 60;
        float fraction = (time * 100) % 100;
        if (fraction > 99) fraction = 99;
        return string.Format("{0}:{1:00}:{2:00}", minutes, seconds, fraction);
    }
}
