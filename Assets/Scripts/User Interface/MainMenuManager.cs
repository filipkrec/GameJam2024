using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : Window
{
    [SerializeField] private SettingsMenuManager _settingsMenu = null;

    [SerializeField] private Button _continueButton = null;
    [SerializeField] private Button _newGameButton = null;
    [SerializeField] private Button _settingsButton = null;
    [SerializeField] private Button _exitButton = null;

    private GameObject _previouslySelectedElement = null;
    private int _lastLevelIndex = -1;

    private void Awake()
    {
        OpenWindow();

        _settingsMenu.SetOnCloseAction(OpenWindow);    // open the main menu after closing settings

        _lastLevelIndex = PlayerPrefs.GetInt(Duck.LAST_LEVEL_KEY, -1);
        if (_lastLevelIndex != -1)
        {
            _continueButton.interactable = true;
            _continueButton.onClick.AddListener(Continue);
        }
        else _continueButton.interactable = false;

        _newGameButton.onClick.AddListener(NewGame);
        _settingsButton.onClick.AddListener(OpenSettings);
        _exitButton.onClick.AddListener(Exit);
    }

    private void OnDestroy()
    {
        _continueButton.onClick.RemoveAllListeners();
        _newGameButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }

    public override void OpenWindow()
    {
        base.OpenWindow();
        GameObject defaultElement = _lastLevelIndex == -1 ? _newGameButton.gameObject : _continueButton.gameObject;
        SetSelectedElement(_previouslySelectedElement != null ? _previouslySelectedElement : defaultElement);
    }

    public override void CloseWindow()
    {
        base.CloseWindow();
    }

    private void Continue()
    {
        AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.UISelect);
        SceneManager.LoadScene(_lastLevelIndex);
    }

    private void NewGame()
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
