using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    private int _roll;

    public int Roll
    {
        get => _roll;
        set
        {
            _roll = value;
            Reset();
        }
    }

    #region Dices

    public event Action<DiceStruct[]> DiceChangedEvent;
    public event Action RollResetEvent;

    private DiceStruct[] _dices = new DiceStruct[6];
    private bool[] _dicesRollFinished = new bool[6];

    public bool AllDiceSet;

    public void ModifyDice(DiceStruct dice)
    {
        ModifyDicesArray(dice);
        Debug.Log($"Dice struct { dice.DiceId} set with value { dice.DiceValue }, and is locked = { dice.IsLocked }. DiceSetCounter = { _dicesRollFinished.Count(x => x == true)}. Roll count { Roll }");

        if (AllDiceSet)
        {
            var lockedDices = GetLockedDices(_dices);

            DiceChangedEvent(lockedDices);
        }
    }

    public void SetRollFinished(DiceStruct dice)
    {
        int diceIndex = GetDiceIndex(dice);

        if (!_dicesRollFinished[diceIndex])
        {
            _dicesRollFinished[diceIndex] = true;
            AllDiceSet = AreAllDiceSet();
        }
    }

    private void Reset()
    {
        ResetDiceSetStatus();

        if (_roll == 0)
        {
            ResetFields();
            RollResetEvent();
        }
    }

    private void ResetFields()
    {
        DiceChangedEvent(new DiceStruct[] { });
    }

    private void ResetDiceSetStatus()
    {
        AllDiceSet = false;
        _dicesRollFinished = new bool[6];
    }

    private DiceStruct[] GetLockedDices(DiceStruct[] dices) => dices.Where(x => x.IsLocked).ToArray();

    private void ModifyDicesArray(DiceStruct dice)
    {
        int diceIndex = GetDiceIndex(dice);

        _dices[diceIndex] = dice;
    }

    private int GetDiceIndex(DiceStruct dice)
    {
        switch (dice.DiceId)
        {
            case DiceId.One:
                return 0;
            case DiceId.Two:
                return 1;
            case DiceId.Three:
                return 2;
            case DiceId.Four:
                return 3;
            case DiceId.Five:
                return 4;
            case DiceId.Six:
                return 5;
            default:
                throw new Exception("DiceId enum not implemented");
        }
    }

    private bool AreAllDiceSet()
    {
        foreach (bool isRollFinished in _dicesRollFinished)
        {
            if (!isRollFinished)
                return false;
        }

        return true;
    }

    #endregion


}
