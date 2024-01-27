using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro _pressAnyKeyPrompt = null;
    [SerializeField] private float _pressAnyKeyPromptDelay = 0.0f;
    [SerializeField] private float _pressAnyKeyPromptAppearDuration = 0.0f;

    private bool _isAcceptingInput = false;
    private Color _pressAnyKeyPromptColor = Color.clear;

    private void Awake()
    {
        _pressAnyKeyPromptColor = _pressAnyKeyPrompt.color;
        _pressAnyKeyPrompt.color = Color.clear;

        Display();
    }

    private void Update()
    {
        if (_isAcceptingInput && Input.anyKeyDown)
        {
            // TODO: animate bubbles
            SceneManager.LoadScene(1); // TODO: move after bubble animation
        }
    }

    private void Display()
    {
        IEnumerator DisplayCoroutine()
        {
            yield return new WaitForSeconds(_pressAnyKeyPromptDelay);
            _pressAnyKeyPrompt
                .DOColor(_pressAnyKeyPromptColor, _pressAnyKeyPromptAppearDuration)
                .OnComplete(() => _isAcceptingInput = true);
        }
        StartCoroutine(DisplayCoroutine());
    }
}
