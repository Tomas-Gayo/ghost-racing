using UnityEngine;
using TMPro;

public class GameHUDTextDisplay : MonoBehaviour
{
    [Tooltip("Reference to the time manager script.")]
    public TimeManager timeManager;
    [Tooltip("Reference to the time manager script.")]
    public GameManager gameManager;

    [Tooltip("Reference to the lap count text UI.")]
    public TMP_Text[] lapTimeText;
    [Tooltip("Reference to the current time text UI.")]
    public TMP_Text currentTimeText;
    [Tooltip("Reference to the best time text UI.")]
    public TMP_Text bestTimeText;
    [Tooltip("Reference to the lap count text UI.")]
    public TMP_Text lapCountText;

    private int lapCount;

    private void Start()
    {
        lapCount = 0;
        bestTimeText.text = timeManager.GetBestTimeString();
    }

    void Update()
    {
        // Set all the texts on screen
        currentTimeText.text = timeManager.GetCurrentTimeString();
        lapCountText.text = gameManager.currentLap.ToString() + " / " + gameManager.totalLaps.ToString();

        // Each time the car finish a lap 
        if (gameManager.isLapCompleted)
        {
            lapTimeText[lapCount].text = timeManager.GetTimeString(lapCount);
            lapCount++;
            // Reset lap 
            gameManager.isLapCompleted = false;
        }
            
    }
}