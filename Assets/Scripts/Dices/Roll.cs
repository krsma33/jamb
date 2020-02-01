using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roll : MonoBehaviour
{
    public GameState GameState;
    public VoidEvent DiceRolled;

    public void RollDice()
    {
        if (GameState.CanRollDice())
        {
            GameState.Roll++;
            DiceRolled.Raise();
            ChangeText();
        }
    }

    private void ChangeText()
    {
        var textObject = gameObject.GetComponentInChildren<Text>();

        textObject.text = $"ROLL x{ GameState.Roll }";
    }
}
