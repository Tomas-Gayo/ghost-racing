using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void HoverButton()
    {
        audioSource.clip = hoverSound;
        audioSource.Play();
    }

    public void PressButton()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
    }
}
