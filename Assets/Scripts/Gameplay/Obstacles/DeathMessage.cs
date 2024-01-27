using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class DeathMessage : MonoBehaviour
{
    [SerializeField] private TextMeshPro _messageText = null;
    [SerializeField] private Vector3 _appearPositionOffset = Vector3.zero;
    [SerializeField] private float _appearAnimationDuration = 0.0f;
    [SerializeField] private float _displayDuration = 0.0f;

    public void SetUpMessage(string message)
    {
        _messageText.text = message;
    }

    public void Appear(Vector3 appearPosition, float appearDelay)
    {
        IEnumerator AppearCoroutine()
        {
            yield return new WaitForSeconds(appearDelay);
            transform
                .DOMove(appearPosition + _appearPositionOffset, appearDelay)
                .OnComplete(Display);
        }
        StartCoroutine(AppearCoroutine());
    }

    private void Display()
    {
        IEnumerator DisplayCoroutine()
        {
            yield return new WaitForSeconds(_displayDuration);
            PopUpWindow.Instance.SetPopUpWindow("", "Try again", RestartLevel, "Return to main menu", ReturnToMainMenu); // TODO: move messages somewhere
        }
        StartCoroutine(DisplayCoroutine());
    }

    private void RestartLevel()
    {
        // TODO: restart level
    }

    private void ReturnToMainMenu()
    {
        // TODO: return to main menu
    }
}
