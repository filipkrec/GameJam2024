using UnityEngine;
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
        // TODO: close the menu and continue the game time
    }

    private void OpenSettings()
    {
        CloseWindow();
        _previouslySelectedElement = _settingsButton.gameObject;
        _settingsMenu.OpenWindow();
    }

    private void Exit()
    {
        PopUpWindow.Instance.SetPopUpWindow("Sure you want to go, go?", "Yes", OnExitConfirmed, "No", OnExitCancelled);   // TODO: save the texts to a scriptable or a file
        _previouslySelectedElement = _exitButton.gameObject;
        CloseWindow();
        PopUpWindow.Instance.OpenWindow();
    }

    private void OnExitConfirmed()
    {
        // TODO: load the main menu
    }

    private void OnExitCancelled()
    {
        OpenWindow();
    }
}
