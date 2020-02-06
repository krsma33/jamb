using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roll : MonoBehaviour
{
    public GameState GameState;
    public VoidEvent DiceRolled;

    public VoidEvent FieldFilledSuccess;
    public VoidEvent FieldFilledScratched;

    private void OnEnable()
    {
        FieldFilledSuccess.EventListeners += HandleFieldFilledEvent;
        FieldFilledScratched.EventListeners += HandleFieldFilledEvent;
    }

    private void OnDisable()
    {
        FieldFilledSuccess.EventListeners -= HandleFieldFilledEvent;
        FieldFilledScratched.EventListeners -= HandleFieldFilledEvent;
    }

    private void HandleFieldFilledEvent()
    {
        ChangeText("NEXT");
    }

    public void RollDice()
    {
        if (GameState.CanRollDice())
        {
            GameState.Roll++;
            DiceRolled.Raise();
            ChangeText(GetRollText());
        }
    }

    private void ChangeText(string currentThrow)
    {
        var textObject = gameObject.GetComponentInChildren<Text>();

        textObject.text = currentThrow;
    }

    private string GetRollText()
    {
        switch (GameState.Roll)
        {
            case 1:
                return "I";
            case 2:
                return "II";
            case 3:
                return "III";
            default:
                return "NEXT";
        }
    }
}
