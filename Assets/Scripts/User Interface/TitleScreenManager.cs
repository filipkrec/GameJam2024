using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [Header("Title Values")]
    [SerializeField] private TextMeshPro _titleText = null;
    [SerializeField] private float _titleTextAppearDuration = 0.0f;

    [Header("Prompt Values")]
    [SerializeField] private TextMeshPro _promptText = null;
    [SerializeField] private float _promptAppearDelay = 0.0f;
    [SerializeField] private float _promptAppearDuration = 0.0f;
    [SerializeField] private Vector2 _promptGlowAlphaRange = Vector2.zero;
    [SerializeField] private float _promptGlowPhaseDuration = 0.0f;

    [Header("Transition Animation Values")]
    [SerializeField] private Transform[] _objectsToMove = null;
    [SerializeField] private Vector3 _transitionOffset = Vector3.zero;
    [SerializeField] private float _transitionDuration = 0.0f;

    private bool _isAcceptingInput = false;
    private Sequence _promptGlowSequence = null;
    private Sequence _transitionSequence = null;

    private void Awake()
    {
        ShowTitleText();
    }

    private void Update()
    {
        if (_isAcceptingInput && Input.anyKeyDown)
        {
            AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.UISelect);
            DOTween.KillAll();

            _transitionSequence = DOTween.Sequence();
            _transitionSequence.Pause();
            foreach (Transform objectToMove in _objectsToMove) _transitionSequence.Join(objectToMove.DOMove(objectToMove.position + _transitionOffset, _transitionDuration));
            _transitionSequence.OnComplete(() => SceneManager.LoadScene("Main Menu"));
            _transitionSequence.Play();

            _isAcceptingInput = false;
        }
    }

    private void ShowTitleText()
    {
        _titleText.DOFade(0.0f, 0.0f);
        _promptText
            .DOFade(0.0f, 0.0f)
            .OnComplete(() => AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.Duck));

        _titleText
            .DOFade(1.0f, _titleTextAppearDuration)
            .OnComplete(() => ShowPrompt());
    }

    private void ShowPrompt()
    {
        IEnumerator ShowPromptCoroutine()
        {
            yield return new WaitForSeconds(_promptAppearDelay);
            _promptText
                .DOFade(_promptGlowAlphaRange.y, _promptAppearDuration)
                .OnComplete(() =>
                {
                    AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.Duck);

                    _isAcceptingInput = true;

                    _promptGlowSequence = DOTween.Sequence();
                    _promptGlowSequence.Pause();
                    _promptGlowSequence
                        .Append(_promptText.DOFade(_promptGlowAlphaRange.x, _promptGlowPhaseDuration).SetEase(Ease.Linear))
                        .Append(_promptText.DOFade(_promptGlowAlphaRange.y, _promptGlowPhaseDuration).SetEase(Ease.Linear));
                    _promptGlowSequence.SetLoops(-1);
                    _promptGlowSequence.Play();
                });
        }
        StartCoroutine(ShowPromptCoroutine());
    }
}
