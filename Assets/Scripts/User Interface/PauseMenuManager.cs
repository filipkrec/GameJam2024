using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : Window
{
    [SerializeField] private SettingsMenuManager _settingsMenu = null;

    [SerializeField] private Button _continueButton = null;
    [SerializeField] private Button _settingsButton = null;
    [SerializeField] private Button _exitButton = null;

    private GameObject _previouslySelectedElement = null;

    private void Awake()
    {
        CloseWindow();

        _settingsMenu.SetOnCloseAction(OpenWindow);    // open the pause menu after closing settings

        _continueButton.onClick.AddListener(Continue);
        _settingsButton.onClick.AddListener(OpenSettings);
        _exitButton.onClick.AddListener(Exit);
    }

    protected override void Update()
    {
        base.Update();

        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && !_isOpened)
        {
            Time.timeScale = 0.0f;
            OpenWindow();
        }
    }

    private void OnDestroy()
    {
        _continueButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }

    public override void OpenWindow()
    {
        base.OpenWindow();
        SetSelectedElement(_previouslySelectedElement != null ? _previouslySelectedElement : _continueButton.gameObject);
    }

    public override void CloseWindow()
    {
        base.CloseWindow();
    }

    private void Continue()
    {
        AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.UISelect);
        Time.timeScale = 1.0f;
        CloseWindow();
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
        PopUpWindow.Instance.SetPopUpWindow("You want to duck out?", "Yes", OnExitConfirmed, "No", OnExitCancelled);
        _previouslySelectedElement = _exitButton.gameObject;
        CloseWindow();
        PopUpWindow.Instance.OpenWindow();
    }

    private void OnExitConfirmed()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main Menu");
    }

    private void OnExitCancelled()
    {
        OpenWindow();
    }
}
