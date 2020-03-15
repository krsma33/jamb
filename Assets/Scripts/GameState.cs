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
    private int _unlockCounter;
    private bool _gameOver;

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

    #region Events

    public event Action<DiceStruct[]> DiceChangedEvent;
    public event Action RollResetEvent;
    public event Action FieldCalledEvent;
    public event Action<bool> ScribbleButtonToggledEvent;

    #endregion
    
    #region Methods

    private DiceStruct[] _dices = new DiceStruct[6];
    private bool[] _dicesRollFinished = new bool[6];

    public bool IsRollFinished;

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
        }

            IsRollFinished = AreAllDiceSet();
    }

    public void RaiseScribbleButtonToggledEvent(bool isToggledOn)
    {
        if (ScribbleButtonToggledEvent != null)
        {
            ScribbleButtonToggledEvent(isToggledOn);
            RaiseDiceChangedEvent();
        }

    }

    public void RaiseFieldCalledEvent()
    {
        FieldCalledEvent();
        RaiseDiceChangedEvent();
    }

    public void GameOver()
    {
        _gameOver = true;
    }

    public bool CanRollDice() => (Roll == 0 || (_unlockCounter > 0 && Roll < 3 && IsRollFinished)) && _dices.Any(x => !x.IsLocked) && !_gameOver;

    public void IncrementUnlockCounter()
    {
        _unlockCounter++;
    }

    private void RaiseDiceChangedEvent()
    {
        if (IsRollFinished)
        {
            _unlockCounter = 0;

            var lockedDices = GetLockedDices(_dices);

            DiceChangedEvent?.Invoke(lockedDices);
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
        _dices = new DiceStruct[6];
        DiceChangedEvent(_dices);
    }

    private void ResetDiceSetStatus()
    {
        IsRollFinished = false;
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
