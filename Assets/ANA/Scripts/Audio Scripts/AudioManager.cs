using UnityEngine;

public class AudioManager : SingletonBehaviour<AudioManager>
{
    [SerializeField] private SoundEffects _soundEffects = null;

    [SerializeField] private AudioSource _soundEffectSource = null;
    [SerializeField] private AudioSource _backgroundMusicSource = null;

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

    public void UpdateSoundEffectsVolume(float volumePercentage)
    {
        _soundEffectSource.volume = volumePercentage / 100.0f;
    }

    public void PlayBackgroundMusic()
    {
        _backgroundMusicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        _backgroundMusicSource.Stop();
    }

    public void UpdateBackgroundMusicVolume(float volumePercentage)
    {
        _backgroundMusicSource.volume = volumePercentage / 100.0f;
    }
}
