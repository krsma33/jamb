using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public int Roll;

    #region Dices

    public int DicesSetCounter;
    public DiceStruct[] Dices;
    public event Action<DiceStruct[], int> RollFinishedEvent;

    private Dictionary<DiceId, DiceStruct> _dices = new Dictionary<DiceId, DiceStruct>();

    public void AddOrModifyDice(DiceStruct dice)
    {
        DicesSetCounter++;
        _dices[dice.DiceId] = dice;
        Debug.Log($"Dice struct { dice.DiceId} set with value { dice.DiceValue }, and is locked = { dice.IsLocked }. DiceSetCounter = {DicesSetCounter}");

        if (AreAllDiceSet())
        {
            DicesSetCounter = 0;
            Roll++;
            OrderDices();
            RaiseRollFinished();
        }
    }

    private bool AreAllDiceSet() => DicesSetCounter < 6 ? false : true;

    private void OrderDices()
    {
        Dices = _dices.Select(kvp => kvp.Value).OrderByDescending(dice => dice.DiceValue).ToArray();
    }

    private void RaiseRollFinished()
    {
        RollFinishedEvent(Dices, Roll);
    }

    #endregion


}
