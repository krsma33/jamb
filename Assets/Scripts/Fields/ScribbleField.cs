using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScribbleField : MonoBehaviour
{
    public GameState GameState;
    public VoidEvent FilledScratchedEvent;


    private bool _isButtonToggled;

    private void OnEnable()
    {
        FilledScratchedEvent.EventListeners += ResetScriblleFieldToggle;
    }

    private void OnDisable()
    {
        FilledScratchedEvent.EventListeners -= ResetScriblleFieldToggle;

    }

    private void ResetScriblleFieldToggle()
    {
        IsButtonToggled = false;
    }

    public bool IsButtonToggled
    {
        get => _isButtonToggled;
        set
        {
            _isButtonToggled = value;

            HighlightButton(_isButtonToggled);
            GameState.RaiseScribbleButtonToggledEvent(_isButtonToggled);
        }
    }

    public void ToggleButton()
    {
        IsButtonToggled = !IsButtonToggled;
    }

    private void HighlightButton(bool highlightButton)
    {
        var image = gameObject.GetComponent<Image>();

        if (_isButtonToggled)
        {
            image.color = Color.red;
        }
        else
        {
            image.color = Color.white;
        }
    }
}
