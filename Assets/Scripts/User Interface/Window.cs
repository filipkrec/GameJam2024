using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Window : MonoBehaviour
{
    [SerializeField] protected GameObject _content = null;

    protected bool _isOpened = false;

    protected virtual void Update()
    {
        if (_isOpened)
        {
            if (Input.GetKeyDown(KeyCode.W)
                || Input.GetKeyDown(KeyCode.UpArrow)
                || Input.GetKeyDown(KeyCode.A)
                || Input.GetKeyDown(KeyCode.LeftArrow)
                || Input.GetKeyDown(KeyCode.S)
                || Input.GetKeyDown(KeyCode.DownArrow)
                || Input.GetKeyDown(KeyCode.D)
                || Input.GetKeyDown(KeyCode.RightArrow))
                AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.UIChangeSelection);
        }
    }

    public virtual void OpenWindow()
    {
        _content.SetActive(true);
        _isOpened = true;
    }

    public virtual void CloseWindow()
    {
        _isOpened = false;
        _content.SetActive(false);
    }

    protected void SetSelectedElement(GameObject element)
    {
        EventSystem.current.SetSelectedGameObject(element);
    }
}
