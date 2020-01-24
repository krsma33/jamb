using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{

    #region Private Members

    private int _roll;

    #endregion

    #region Properties

    public int Roll
    {
        get => _roll;
        set
        {
            _roll = value;
            Reset();
        }
    }

    #endregion

    #region Dices

    public event Action<DiceStruct[]> DiceChangedEvent;
    public event Action RollResetEvent;
    public event Action FieldCalledEvent;
    public event Action<bool> ScribbleButtonToggledEvent;

    private DiceStruct[] _dices = new DiceStruct[6];
    private bool[] _dicesRollFinished = new bool[6];

    public bool AllDiceSet;

    public void ModifyDice(DiceStruct dice)
    {
        ModifyDicesArray(dice);
        RaiseDiceChangedEvent();
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

    public void RaiseScribbleButtonToggledEvent(bool isToggledOn)
    {
        ScribbleButtonToggledEvent(isToggledOn);
        RaiseDiceChangedEvent();
    }

    public void RaiseFieldCalledEvent()
    {
        FieldCalledEvent();
        RaiseDiceChangedEvent();
    }

    private void RaiseDiceChangedEvent()
    {
        if (AllDiceSet)
        {
            var lockedDices = GetLockedDices(_dices);

            DiceChangedEvent(lockedDices);
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
