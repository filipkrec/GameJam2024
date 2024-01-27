using UnityEngine;
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
        // TODO: load first level
    }

    private void OpenSettings()
    {
        CloseWindow();
        _previouslySelectedElement = _settingsButton.gameObject;
        _settingsMenu.OpenWindow();
    }

    private void Exit()
    {
        PopUpWindow.Instance.SetPopUpWindow("Sure you want to duck out?", "Yes", OnExitConfirmed, "No", OnExitCancelled);   // TODO: save the texts to a scriptable or a file
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
