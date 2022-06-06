using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotOptionsMenu : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] private GameObject _buttonsContainer;

    private bool _visible;

    public void OnPointerExit(PointerEventData eventData)
    {
        SetVisible(false);
    }

    public void SetVisible(bool visible)
    {
        if (visible == _visible) return;

        if(visible == false)
        {
            _buttonsContainer.SetActive(false);
        }
        else
        {
            _buttonsContainer.SetActive(true);
        }

        _visible = visible;
    }

    public void ToggleVisible()
    {
        SetVisible(!_visible);
    }
}
