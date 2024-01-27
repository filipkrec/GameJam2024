using UnityEngine;

public class AudioManager : SingletonBehaviour<AudioManager>
{
    private const string _PREFS_SOUND_EFFECTS_VOLUME_KEY = "SoundEffectsVolume";
    private const string _PREFS_BACKGROUND_MUSIC_VOLUME_KEY = "BackgroundMusicVolume";

    [SerializeField] private SoundEffects _soundEffects = null;

    [SerializeField] private AudioSource _soundEffectSource = null;
    [SerializeField] private AudioSource _backgroundMusicSource = null;

    public float SoundEffectsVolume => PlayerPrefs.GetFloat(_PREFS_SOUND_EFFECTS_VOLUME_KEY, 1.0f);         // maximum volume if there's no saved data
    public float BackgroundMusicVolume => PlayerPrefs.GetFloat(_PREFS_BACKGROUND_MUSIC_VOLUME_KEY, 1.0f);   // maximum volume if there's no saved data

    protected override void Awake()
    {
        base.Awake();

        _soundEffectSource.volume = SoundEffectsVolume;
        _backgroundMusicSource.volume = BackgroundMusicVolume;
    }

    public void PlaySoundEffectByType(SoundEffectType type)
    {
        AudioClip clip = _soundEffects.GetSoundEffectByType(type);
        if (clip == null)
        {
            Debug.LogWarning("Warning: no data for sound effect of type " + type.ToString());
            return;
        }
        PlaySoundEffectFromClip(clip);
    }

    public void PlaySoundEffectFromClip(AudioClip clip)
    {
        _soundEffectSource.clip = clip;
        _soundEffectSource.Play();
    }

    public void StopSoundEffect()
    {
        _soundEffectSource.Stop();
    }

    public void UpdateSoundEffectsVolume(float volume)
    {
        _soundEffectSource.volume = volume;
        PlayerPrefs.SetFloat(_PREFS_SOUND_EFFECTS_VOLUME_KEY, volume);
    }

    public void PlayBackgroundMusic()
    {
        _backgroundMusicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        _backgroundMusicSource.Stop();
    }

    public void UpdateBackgroundMusicVolume(float volume)
    {
        _backgroundMusicSource.volume = volume;
        PlayerPrefs.SetFloat(_PREFS_BACKGROUND_MUSIC_VOLUME_KEY, volume);
    }
}
