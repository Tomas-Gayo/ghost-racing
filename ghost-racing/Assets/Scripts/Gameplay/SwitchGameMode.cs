using UnityEngine;
using Cinemachine;

public class SwitchGameMode : MonoBehaviour
{
    [Tooltip("Reference to the Game Manager script.")]
    public GameManager gameManager;
    [Tooltip("Reference to the Replay Race Manager script.")]
    public ReplayRaceManager replayRaceManager;
    [Tooltip("The HUD game object of the game, so it is possible to disable in other modes.")]
    public GameObject HUD;
    [Tooltip("Reference to the objects used to block cameras and allow switching.")]
    public GameObject obstacles;
    [Tooltip("Reference to the object of the finish line.")]
    public GameObject finishLine;
    [Tooltip("Reference to the Checkpoints of the race.")]
    public GameObject checkpoints;
    [Tooltip("Reference to the camera that follows the car.")]
    public GameObject carCamera;
    [Tooltip("Reference to the cameras that are displayed around the circuit.")]
    public GameObject replayCam;

    void Start()
    {
        if (GameConfiguration.mode == 1)
        {
            gameManager.enabled = true;
            HUD.SetActive(true);
            finishLine.SetActive(true);
            checkpoints.SetActive(true);
            carCamera.SetActive(true);
        }
        else if (GameConfiguration.mode == 2)
        {
            replayRaceManager.enabled = true;
            obstacles.SetActive(true);
            replayCam.SetActive(true);
        }
    }

}

