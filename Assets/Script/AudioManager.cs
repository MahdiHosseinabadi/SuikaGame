using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    DropSound,
    MergeSound,
    Break,
    ButtonSound,
    NewRecordSound,
    GameOverSound
}

[System.Serializable]
public class Sound
{
    public SoundType type;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.5f, 2f)]
    public float pitch;

    [Tooltip("Minimum interval between two audio playing")]
    public float coolDown = 0f;
}

public class AudioManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;
    
    public Sound[] sounds;

    Dictionary<SoundType, Sound> SoundDictionary;
    Dictionary<SoundType, float> LastPlayTime;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SoundDictionary = new Dictionary<SoundType, Sound>();
        LastPlayTime = new Dictionary<SoundType, float>();

        foreach (Sound sound in sounds)
        {
            SoundDictionary.Add(sound.type, sound);
            LastPlayTime.Add(sound.type, -999f);
        }
    }

    public void PlaySound(SoundType type)
    {
        if (!SoundDictionary.ContainsKey(type)) return;

        Sound sound = SoundDictionary[type];

        if (Time.time - LastPlayTime[type] < sound.coolDown) return;

        LastPlayTime[type] = Time.time;

        sfxSource.pitch = sound.pitch;
        sfxSource.PlayOneShot(sound.clip, sound.volume);
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }
}