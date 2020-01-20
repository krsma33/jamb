using ScriptableObjectEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int DiceId;
    public DiceEvent ValueChanged;

    private int diceValue;
    private bool diceLocked = false;

    public void ToggleLock()
    {
        diceLocked = !diceLocked;
    }

    public void RollDice()
    {
        diceValue = Random.Range(1, 7);

        DiceStruct dice = new DiceStruct(DiceId, diceValue, diceLocked);

        ValueChanged.Raise(dice);
    }
}
