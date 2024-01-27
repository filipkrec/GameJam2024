using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Window : MonoBehaviour
{
    [SerializeField] protected GameObject _content = null;

    public virtual void OpenWindow()
    {
        _content.SetActive(true);
    }

    public virtual void CloseWindow()
    {
        _content.SetActive(false);
    }

    protected void SetSelectedElement(GameObject element)
    {
        EventSystem.current.SetSelectedGameObject(element);
    }
}
