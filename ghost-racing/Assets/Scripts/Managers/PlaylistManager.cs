using System.Collections.Generic;
using UnityEngine;

public class PlaylistManager : MonoBehaviour
{
    public static AudioSource audioSource;
    public List<Songs> songs = new List<Songs>();

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySong(songs[Random.Range(0, songs.Count)]);
    }

    public void PlaySong(Songs songs)
    {
        audioSource.clip = songs.audioClip;
        audioSource.Play();
    }
}
