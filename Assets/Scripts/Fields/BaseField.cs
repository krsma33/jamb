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
    #region Private Members

    private bool isFilled;
    private int[] diceValues;

    #endregion

    #region Protected Members

    protected int Roll;

    #endregion

    #region Public Members

    public GameState GameState;
    public RowType Row;

    #endregion

    #region Abstract Class Implementation

    private void Awake()
    {
        GameState.RollFinishedEvent += RollFinishedHandler;
    }

    private void RollFinishedHandler(DiceStruct[] dices, int roll)
    {
        string diceComb = dices.Select(x => x.DiceValue.ToString()).Aggregate((x, y) => $"{x},{y}");
        Debug.Log(diceComb + ". Roll No: " + roll);

        diceValues = dices.Select(kvp => kvp.DiceValue).ToArray();
        Roll = roll;

        if (IsFillable())
        {
            gameObject.GetComponent<Image>().color = Color.cyan;
        }
    }

    private bool IsNumbersFieldFillable(int rowNumber) => !diceValues.Contains(rowNumber) ? false : true;

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
        foreach (var kvp in GetValueCounts())
        {
            if (kvp.Value >= expectedDiceNumber)
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

    private bool AreFieldFillConditionsMet(Func<bool> rowSpecificPredicate)
    {
        if (isFilled)
            return false;

        if (Roll == 0)
            return false;

        if (!IsColumnSpecificConditionMet())
            return false;

        if (!rowSpecificPredicate())
            return false;

        return true;
    }

    public bool IsFillable()
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
                isFillable = AreFieldFillConditionsMet(() => true);
                break;
            case RowType.Min:
                isFillable = AreFieldFillConditionsMet(() => true);
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
                break;
        }

        return isFillable;
    }

    #endregion

    #region Abstract Methods

    protected abstract bool IsColumnSpecificConditionMet();

    #endregion
}
