using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : Window
{
    [SerializeField] private SettingsMenuManager _settingsMenu = null;

    [SerializeField] private Button _playButton = null;
    [SerializeField] private Button _settingsButton = null;
    [SerializeField] private Button _exitButton = null;

    private GameObject _previouslySelectedElement = null;

    private void Awake()
    {
        OpenWindow();

        _settingsMenu.SetOnCloseAction(OpenWindow);    // open the main menu after closing settings

        _playButton.onClick.AddListener(Play);
        _settingsButton.onClick.AddListener(OpenSettings);
        _exitButton.onClick.AddListener(Exit);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }

    public override void OpenWindow()
    {
        base.OpenWindow();
        SetSelectedElement(_previouslySelectedElement != null ? _previouslySelectedElement : _playButton.gameObject);
    }

    public override void CloseWindow()
    {
        base.CloseWindow();
    }

    private void Play()
    {
        AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.UISelect);
        SceneManager.LoadScene("Level1");
    }

    private void OpenSettings()
    {
        AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.UISelect);
        CloseWindow();
        _previouslySelectedElement = _settingsButton.gameObject;
        _settingsMenu.OpenWindow();
    }

    private void Exit()
    {
        AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.UISelect);
        PopUpWindow.Instance.SetPopUpWindow("Sure you want to go-go?", "Yes", OnExitConfirmed, "No", OnExitCancelled);
        _previouslySelectedElement = _exitButton.gameObject;
        CloseWindow();
        PopUpWindow.Instance.OpenWindow();
    }

    private void OnExitConfirmed()
    {
        Application.Quit();
    }

    private void OnExitCancelled()
    {
        OpenWindow();
    }
}
