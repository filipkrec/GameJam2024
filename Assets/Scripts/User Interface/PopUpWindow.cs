using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWindow : Window
{
    private static PopUpWindow _instance = null;

    public static PopUpWindow Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<PopUpWindow>();
            return _instance;
        }
    }

    [SerializeField] private TextMeshProUGUI _messageTextField = null;

    [SerializeField] private TextMeshProUGUI _optionOneTextField = null;
    [SerializeField] private Button _optionOneButton = null;

    [SerializeField] private TextMeshProUGUI _optionTwoTextField = null;
    [SerializeField] private Button _optionTwoButton = null;

    private Action _onOptionOneAction = null;
    private Action _onOptionTwoAction = null;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else if (!_instance.Equals(this))
        {
            Destroy(gameObject);
            return;
        }

        CloseWindow();

        _optionOneButton.onClick.AddListener(SelectOptionOne);
        _optionTwoButton.onClick.AddListener(SelectOptionTwo);
    }

    public override void OpenWindow()
    {
        base.OpenWindow();
        SetSelectedElement(_optionOneButton.gameObject);
    }

    public void SetPopUpWindow(string message, string optionOneText, Action onOptionOneAction, string optionTwoText, Action onOptionTwoAction)
    {
        _messageTextField.text = message;

        _optionOneTextField.text = optionOneText;
        _onOptionOneAction = onOptionOneAction;

        _optionTwoTextField.text = optionTwoText;
        _onOptionTwoAction = onOptionTwoAction;
    }

    private void SelectOptionOne()
    {
        AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.UISelect);
        CloseWindow();
        _onOptionOneAction?.Invoke();
    }

    private void SelectOptionTwo()
    {
        AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.UISelect);
        CloseWindow();
        _onOptionTwoAction?.Invoke();
    }
}
