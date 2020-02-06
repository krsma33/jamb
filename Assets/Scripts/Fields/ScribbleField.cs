using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScribbleField : MonoBehaviour
{
    public GameState GameState;
    public VoidEvent FilledScratchedEvent;
    public Image Icon;
    public Color DefaultColor;

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
        if (_isButtonToggled)
        {
            Icon.color = Color.white;
        }
        else
        {
            Icon.color = DefaultColor;
        }
    }
}
