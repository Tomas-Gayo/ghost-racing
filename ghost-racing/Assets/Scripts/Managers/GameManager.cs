using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [Tooltip("The delay between the start of the scene and the start of the race.")]
    public float startDelay = 0.5f;                   
    [Tooltip("The delay after the end of the race and the next action.")]
    public float endDelay = 3f;
    [Tooltip("Counter delay used for the countdown (1 second by default).")]
    public float counterDelay = 1f;
    [Tooltip("The number of laps the player has to complete.")]
    public int totalLaps;
    [Tooltip("Countdown object displayed right before the race starts (3..2..1..GO!).")]
    public GameObject countDown;                    
    [Tooltip("Reference to the prefab car the player will control.")]
    public GameObject carPrefab;
    [Tooltip("Reference to the prefab ghost car.")]
    public GameObject ghostCarPrefab;
    [Tooltip("Reference to the car manager to be able to perform specifc actions about the car.")]
    public CarManager car;
    [Tooltip("Reference to the car manager to be able to perform specifc actions about the car.")]
    public GhostCarManager ghostCar;
    [Tooltip("Referece to the scriptable object where the data of the lap will be saved.")]
    public GhostLapData bestLapSO;
    [Tooltip("Reference to the time manager to be able to control time durning the game.")]
    public TimeManager timeManager;
    [Tooltip("Reference to the audio played on the countdown (3..2..1)")]
    public AudioClip countAudio;
    [Tooltip("Reference to the audio played on the countdown (GO!)")]
    public AudioClip goAudio;


    [HideInInspector] public int currentLap;                            // Number of laps done by the car
    [HideInInspector] public int checkpointIndex;                       // Current number of the checkbox hit by the car (a checkpoint cannot be 
    [HideInInspector] public bool isLapCompleted = false;               // lap is finished or not?
    [HideInInspector] public bool isRaceFinished = false;               // Race is finished or not?

    private WaitForSeconds startWait;                                   // Variable used to wait before the race starts
    private WaitForSeconds endWait;                                     // Variable used to wait after the race finishes
    private WaitForSeconds counterWait;                                 // Variable used in the countdown
    private CinemachineVirtualCamera carVCam;                           // Reference to the player camera
    private AudioSource audioSource;                                    // Reference to the audio source of the GameManager

    private void Start()
    {
        // We have the delays created at the beggining so we don't have to generate them again
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay); 
        counterWait = new WaitForSeconds(counterDelay);

        timeManager.GetBestTimeString();

        carVCam = GameObject.Find("Car VCam").GetComponent<CinemachineVirtualCamera>();
        audioSource = gameObject.GetComponent<AudioSource>();

        SpawnCar();
        if (bestLapSO.IsBestData())
            SpawnGhostCar();

        // The game starts until all the laps are completed properly
        StartCoroutine(GameSequence());
    }

    private void SpawnCar()
    {
        car.instance =
            Instantiate(carPrefab, car.spawnPoint.position, car.spawnPoint.rotation);
        car.Setup();
        // Inmediately we disable the control
        DisabledCarControl();
        carVCam.LookAt = car.instance.transform;
        carVCam.Follow = car.instance.transform;

    }

    private void SpawnGhostCar()
    {
        ghostCar.instance =
            Instantiate(ghostCarPrefab, car.spawnPoint.position, car.spawnPoint.rotation);
        ghostCar.Setup();
        DisabledGhostControl();
    }

    // The game is in loop waiting for the user to complete the total of laps 
    private IEnumerator GameSequence()
    {
        yield return StartCoroutine(PrepareRace());

        yield return StartCoroutine(RacePlaying());

        yield return StartCoroutine(EndRace());

        SceneManager.LoadScene(0);
    }

    private IEnumerator PrepareRace()
    {
        // The lap start at 1 
        currentLap = 1;

        // Continue only when the countdown is finished
        yield return StartCoroutine(StartCountdown());
    }

    private IEnumerator RacePlaying()
    {
        // Start tracking time
        timeManager.enabled = true;

        // Car can move
        EnableCarControl();

        // if exists ghost car can move
        if (ghostCar.instance != null)
            EnableGhostControl();

        // Stay here while the race is not finished
        while (!isRaceFinished)
            yield return null;
    }

    private IEnumerator EndRace()
    {
        DisabledCarControl();
        yield return endWait;
    }

    // The countdown 3..2..1..GO!
    private IEnumerator StartCountdown()
    {
        TMP_Text counter = countDown.GetComponentInChildren<TMP_Text>();

        yield return startWait;

        // Countdown: 3
        counter.text = "3";
        countDown.SetActive(true);
        CountdownAudio(countAudio);
        yield return counterWait;
        countDown.SetActive(false);

        // Countdown: 2
        counter.text = "2";
        countDown.SetActive(true);
        CountdownAudio(countAudio);
        yield return counterWait;
        countDown.SetActive(false);

        // Countdown: 1
        counter.text = "1";
        countDown.SetActive(true);
        CountdownAudio(countAudio);
        yield return counterWait;
        countDown.SetActive(false);

        // Countdown: GO!
        counter.text = "GO!";
        countDown.SetActive(true);
        CountdownAudio(goAudio);
        yield return counterWait;
        countDown.SetActive(false);
    }

    // Coundown audio
    private void CountdownAudio(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }

    // Enable and disable car and ghost car controls
    private void EnableCarControl()
    {
        car.EnableControl();
    }

    private void DisabledCarControl()
    {
        car.DisableContol();
    }

    private void EnableGhostControl()
    {
        ghostCar.EnableControl();
    }

    private void DisabledGhostControl()
    {
        ghostCar.DisableContol();
    }
}
