using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    public AudioSource musicSource;      // für Musik (loop)
    public AudioSource sfxSource;        // für OneShots (Jump/Pick/Damage)
    public AudioSource walkSource;       // extra Source nur für Walk-Loop

    [Header("Music")]
    public AudioClip backgroundMusic;

    [Header("SFX")]
    public AudioClip sfxJump;
    public AudioClip sfxPick;
    public AudioClip sfxDamage;
    public AudioClip sfxLose;

    [Header("Walk")]
    public AudioClip sfxWalk;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            if (!musicSource.isPlaying) musicSource.Play();
        }

        // Walk Source vorbereiten
        if (walkSource != null)
        {
            walkSource.loop = true;
            walkSource.playOnAwake = false;
        }
    }

    private void PlayOneShot(AudioClip clip)
    {
        if (sfxSource == null || clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayJump()   => PlayOneShot(sfxJump);
    public void PlayPick()   => PlayOneShot(sfxPick);
    public void PlayDamage() => PlayOneShot(sfxDamage);
    public void PlayLose()   => PlayOneShot(sfxLose);

    public void StartWalk()
    {
        if (walkSource == null || sfxWalk == null) return;
        if (walkSource.clip != sfxWalk) walkSource.clip = sfxWalk;
        if (!walkSource.isPlaying) walkSource.Play();
    }

    public void StopWalk()
    {
        if (walkSource == null) return;
        if (walkSource.isPlaying) walkSource.Stop();
    }
}