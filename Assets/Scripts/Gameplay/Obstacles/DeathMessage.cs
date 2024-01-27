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
            transform.position = appearPosition + _appearPositionOffset;

            Color originalColor = _messageText.color;
            _messageText.color = Color.clear;

            yield return new WaitForSeconds(appearDelay);
            /*
            transform
                .DOMove(appearPosition + _appearPositionOffset, _appearAnimationDuration)
                .OnComplete(Display);
            */
            _messageText // TODO: return the call above when an object is done
                .DOColor(originalColor, _appearAnimationDuration)
                .OnComplete(Display);
        }
        StartCoroutine(AppearCoroutine());
    }

    private void Display()
    {
        IEnumerator DisplayCoroutine()
        {
            yield return new WaitForSeconds(_displayDuration);
            Debug.LogError("SHould open pop up");
            //PopUpWindow.Instance.SetPopUpWindow("", "Try again", RestartLevel, "Return to main menu", ReturnToMainMenu); // TODO: move messages somewhere
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
