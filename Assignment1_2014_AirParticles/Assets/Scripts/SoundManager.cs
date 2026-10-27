
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip backgroundMusic;
    public AudioClip pulseSound;
    public AudioClip hitSound;
    public AudioClip pickupSound;
    public AudioClip redDestroyedSound;

    private AudioSource musicSource;
    private AudioSource soundSource;

    void Awake()
    {
        // Prevents multiple SoundManagers from existing. 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Keeps the music playing when switching scenes.
        DontDestroyOnLoad(gameObject);

        // bg music
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = 0.4f;

        // Sfx 
        soundSource = gameObject.AddComponent<AudioSource>();
        soundSource.playOnAwake = false;
        soundSource.volume = 1f;

        // starts bg music
        if (backgroundMusic != null)
        {
            musicSource.Play();
        }
    }

    // pulse sfx
    public void PlayPulse()
    {
        if (pulseSound != null)
        {
            soundSource.PlayOneShot(pulseSound);
        }
    }

    // hit sfx
    public void PlayHit()
    {
        if (hitSound != null)
        {
            soundSource.PlayOneShot(hitSound);
        }
    }

    // collect sfx
    public void PlayPickup()
    {
        if (pickupSound != null)
        {
            soundSource.PlayOneShot(pickupSound);
        }
    }

    // destroyed sfx
    public void PlayRedDestroyed()
    {
        if (redDestroyedSound != null)
        {
            soundSource.PlayOneShot(redDestroyedSound);
        }
    }
}
