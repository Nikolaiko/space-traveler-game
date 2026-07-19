using UnityEngine;

public class SimpleSoundService : MonoBehaviour, SoundService
{
    private AudioSource musicSource;

    public void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void playMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();    
        }
    }

    public void stopMusic()
    {
        musicSource.Stop();
    }
}
