using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScribbleField : MonoBehaviour
{
    public GameState GameState;

    private bool _isButtonToggled;

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
