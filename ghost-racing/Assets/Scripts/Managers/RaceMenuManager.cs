using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceMenuManager : MonoBehaviour
{
    [Tooltip("Name of the game scene.")]
    public string SceneName;
    [Tooltip("Sound of the button on click.")]
    public AudioClip startGameSound;
    [Tooltip("Referece to the scriptable object where the data of the lap will be saved.")]
    public GhostLapData bestLapSO;

    private AudioSource audioSource;            // Reference to the audio source of the manager
  

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartRace()
    {
        GameConfiguration.mode = 1;
        StartCoroutine(LoadGameScene());
    }

    public void ReplayRace()
    {
        if (bestLapSO.IsBestData())
        {
            GameConfiguration.mode = 2;
            StartCoroutine(LoadGameScene());
        }
    }

    public void QuitGame()
    {
       Application.Quit();
    }

    IEnumerator LoadGameScene()
    {
        audioSource.clip = startGameSound;
        audioSource.Play();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadSceneAsync(SceneName);
    }
}
