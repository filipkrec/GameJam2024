using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : Window
{
    [SerializeField] private Slider _soundEffectVolumeSlider = null;
    [SerializeField] private Slider _backgroundMusicVolumeSlider = null;
    [SerializeField] private Button _backButton = null;

    private Action _onCloseAction = null;

    private void Awake()
    {
        CloseWindow();
    }

    private void Start()
    {
        _soundEffectVolumeSlider.value = AudioManager.Instance.SoundEffectsVolume;
        _soundEffectVolumeSlider.onValueChanged.AddListener(UpdateSoundEffectsVolume);

        _backgroundMusicVolumeSlider.value = AudioManager.Instance.BackgroundMusicVolume;
        _backgroundMusicVolumeSlider.onValueChanged.AddListener(UpdateBackgroundMusicVolume);

        _backButton.onClick.AddListener(Back);
    }

    private void OnDestroy()
    {
        _soundEffectVolumeSlider.onValueChanged.RemoveAllListeners();
        _backgroundMusicVolumeSlider.onValueChanged.RemoveAllListeners();
        _backButton.onClick.RemoveAllListeners();
    }

    public override void OpenWindow()
    {
        base.OpenWindow();
        SetSelectedElement(_soundEffectVolumeSlider.gameObject);
    }

    public void SetOnCloseAction(Action onCloseAction)
    {
        _onCloseAction = onCloseAction;
    }

    private void UpdateSoundEffectsVolume(float value)
    {
        AudioManager.Instance.UpdateSoundEffectsVolume(value);
    }

    private void UpdateBackgroundMusicVolume(float value)
    {
        AudioManager.Instance.UpdateBackgroundMusicVolume(value);
    }

    private void Back()
    {
        AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.UISelect);
        CloseWindow();
        _onCloseAction?.Invoke();
    }
}
