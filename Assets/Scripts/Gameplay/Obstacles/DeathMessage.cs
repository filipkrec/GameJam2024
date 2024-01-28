using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            _messageText
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
            PopUpWindow.Instance.SetPopUpWindow("Want to try again or will you chicken out?", "Restart", RestartLevel, "Main Menu", ReturnToMainMenu);
            PopUpWindow.Instance.OpenWindow();
        }
        StartCoroutine(DisplayCoroutine());
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
