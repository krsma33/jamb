using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public enum RowType
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Max,
    Min,
    Kenta,
    Trilling,
    FullHouse,
    Poker,
    Jamb
}

public abstract class BaseField : MonoBehaviour
{

    #region Abstract Class Implementation

    #region Private Members

    private int[] diceValues;

    #endregion

    #region Protected Members

    protected bool isFilled = false;
    protected bool isFillable;
    protected int roll;

    #endregion

    #region Public Members

    public GameState GameState;
    public RowType Row;

    #endregion

    private void Awake()
    {
        GameState.DiceChangedEvent += DiceChangedHandler;
    }

    private void DiceChangedHandler(DiceStruct[] dices)
    {
        ResetButton();

        roll = GameState.Roll;

        HighlightLogic(dices);
    }

    #region Fill Logic

    public void FillField()
    {
        if (!isFilled)
        {
            FillLogic();
        }
    }

    protected virtual void FillLogic()
    {
        if (isFillable)
            ValueFill();
    }

    private void ValueFill()
    {
        int fieldValue = GetFieldValue();
        gameObject.GetComponentInChildren<Text>().text = fieldValue.ToString();

        isFilled = true;
        GameState.DiceChangedEvent -= DiceChangedHandler;

        // notify sum of the change

        GameState.Roll = 0;

        // reset collor
        ResetButton();
    }

    private int GetDiceSum()
    {
        if (diceValues.Length > 0)
             return diceValues.Sum();

        return 0;
    }

    private int GetKentaValue()
    {
        if (roll > 2)
            return 66;

        if (roll == 2)
            return 56;

        if (roll == 1)
            return 46;

        return 0;
    }

    private int GetCombinationSumWithBonus(int bonus)
    {
        if (diceValues.Length > 0)
            return GetDiceSum() + bonus;

        return 0;
    }

    private int GetFieldValue()
    {
        switch (Row)
        {
            case RowType.One:
                return GetDiceSum();
            case RowType.Two:
                return GetDiceSum();
            case RowType.Three:
                return GetDiceSum();
            case RowType.Four:
                return GetDiceSum();
            case RowType.Five:
                return GetDiceSum();
            case RowType.Six:
                return GetDiceSum();
            case RowType.Max:
                return GetDiceSum();
            case RowType.Min:
                return GetDiceSum();
            case RowType.Kenta:
                return GetKentaValue();
            case RowType.Trilling:
                return GetCombinationSumWithBonus(20);
            case RowType.FullHouse:
                return GetCombinationSumWithBonus(30);
            case RowType.Poker:
                return GetCombinationSumWithBonus(40);
            case RowType.Jamb:
                return GetCombinationSumWithBonus(50);
            default:
                throw new Exception("Not implemented RowType enum");
        }
    }

    #endregion

    #region Highlight Logic

    private void ResetButton()
    {
        isFillable = false;
        diceValues = null;
        gameObject.GetComponent<Image>().color = Color.white;
    }

    private void HighlightFillable()
    {
        gameObject.GetComponent<Image>().color = Color.cyan;
    }

    protected virtual void HighlightLogic(DiceStruct[] dices)
    {
        if (dices.Length > 0)
        {
            diceValues = dices.Select(x => x.DiceValue).OrderByDescending(x => x).ToArray();

            isFillable = IsFillable();

            if (isFillable)
                HighlightFillable();
        }
    }

    private bool AreFieldFillConditionsMet(Func<bool> rowSpecificPredicate)
    {
        if (isFilled)
            return false;

        if (roll == 0)
            return false;

        if (!IsColumnSpecificConditionMet())
            return false;

        if (!rowSpecificPredicate())
            return false;

        return true;
    }

    private bool IsFillable()
    {
        bool isFillable = false;

        switch (Row)
        {
            case RowType.One:
                isFillable = AreFieldFillConditionsMet(() => IsNumbersFieldFillable(1));
                break;
            case RowType.Two:
                isFillable = AreFieldFillConditionsMet(() => IsNumbersFieldFillable(2));
                break;
            case RowType.Three:
                isFillable = AreFieldFillConditionsMet(() => IsNumbersFieldFillable(3));
                break;
            case RowType.Four:
                isFillable = AreFieldFillConditionsMet(() => IsNumbersFieldFillable(4));
                break;
            case RowType.Five:
                isFillable = AreFieldFillConditionsMet(() => IsNumbersFieldFillable(5));
                break;
            case RowType.Six:
                isFillable = AreFieldFillConditionsMet(() => IsNumbersFieldFillable(6));
                break;
            case RowType.Max:
                isFillable = AreFieldFillConditionsMet(() => diceValues.Length == 5);
                break;
            case RowType.Min:
                isFillable = AreFieldFillConditionsMet(() => diceValues.Length == 5);
                break;
            case RowType.Kenta:
                isFillable = AreFieldFillConditionsMet(() => IsKentaFieldFillable());
                break;
            case RowType.Trilling:
                isFillable = AreFieldFillConditionsMet(() => IsTrillingFieldFillable());
                break;
            case RowType.FullHouse:
                isFillable = AreFieldFillConditionsMet(() => IsFullHouseFieldFillable());
                break;
            case RowType.Poker:
                isFillable = AreFieldFillConditionsMet(() => IsPokerFieldFillable());
                break;
            case RowType.Jamb:
                isFillable = AreFieldFillConditionsMet(() => IsJambFieldFillable());
                break;
            default:
                throw new Exception("Not implemented RowType enum");
        }

        return isFillable;
    }

    private bool IsNumbersFieldFillable(int rowNumber)
    {
        if (diceValues.Length > 5)
            return false;

        return !diceValues.All(x => x == rowNumber) ? false : true;
    }

    private bool IsKentaFieldFillable()
    {
        if (diceValues[0] < 5)
            return false;

        for (int i = diceValues.Length - 1; i > 0; i--)
        {
            if (diceValues[i] != diceValues[i - 1] + 1)
                return false;
        }

        return true;
    }

    private bool IsFullHouseFieldFillable()
    {
        var diceCounts = GetValueCounts();

        var diceCountValues = diceCounts.Select(kvp => kvp.Value).OrderByDescending(x => x).ToList();

        if (diceCountValues[0] == 3 && diceCountValues[1] == 2)
            return true;

        return false;
    }

    private bool IsTrillingFieldFillable()
    {
        return IsSpecifiedNumberOfSameDiceFound(3);
    }

    private bool IsPokerFieldFillable()
    {
        return IsSpecifiedNumberOfSameDiceFound(4);
    }

    private bool IsJambFieldFillable()
    {
        return IsSpecifiedNumberOfSameDiceFound(5);
    }

    private bool IsSpecifiedNumberOfSameDiceFound(int expectedDiceNumber)
    {
        if (diceValues.Length != expectedDiceNumber)
            return false;

        foreach (var kvp in GetValueCounts())
        {
            if (kvp.Value == expectedDiceNumber)
                return true;
        }

        return false;
    }

    private Dictionary<int, int> GetValueCounts()
    {
        var valueCounts = new Dictionary<int, int>();

        for (int i = 1; i < 7; i++)
        {
            int sameValueCounter = 0;

            for (int j = 0; j < diceValues.Length; j++)
            {
                if (i == diceValues[j])
                    sameValueCounter++;
            }

            valueCounts[i] = sameValueCounter;
        }

        return valueCounts;
    }

    #endregion

    #region Abstract Methods

    protected abstract bool IsColumnSpecificConditionMet();

    #endregion

    #endregion

}
