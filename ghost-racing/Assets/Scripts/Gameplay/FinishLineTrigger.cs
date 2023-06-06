using System.Collections.Generic;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    [Tooltip("Reference to the time manager script.")]
    public TimeManager time;   
    [Tooltip("Reference to the game manager script.")]
    public GameManager gameManager;
    [Tooltip("Referece to the scriptable object where the data of the lap will be saved.")]
    public GhostLapData bestLapSO;

    public List<Checkpoint> checkpoints;                // List with all the chekpoints on the circuit

    private int totalLaps;                              // Maximum of laps the car has to complet

    private void Start()
    {
        totalLaps = gameManager.totalLaps;
    }

    // When the car goes through the finish line ...
    private void OnTriggerEnter(Collider other)
    {
        // ... if all the checkpoints are check ...
        if (other.gameObject.CompareTag("Player") && IsLapComplete())
        {
            // ... save the current time.
            time.SetTime();

            // Reset the lap to its default state if race is not finished.
            if (gameManager.currentLap < totalLaps)
            {
                ResetLap();
                Debug.Log("The current lap is: " + gameManager.currentLap);
            }
            else
            { 
                // otherwise, finish the race
                FinishRace();
            }
        }
    }

    // Depending on the quantity of checkpoints gathered we know if the player has go through all the checkpoints or not
    private bool IsLapComplete()
    {     
        return gameManager.isLapCompleted = (checkpoints.Count == gameManager.checkpointIndex); ;
    }

    private void ResetLap()
    {
        time.SetPreviousTime();
        time.isBestTime = false;
        gameManager.checkpointIndex = 0;
        gameManager.currentLap++;
    }

    private void FinishRace()
    {
        time.Stop();
        time.SetFinalTime();
        bestLapSO.Reset();
        gameManager.isRaceFinished = true;
    }
}
