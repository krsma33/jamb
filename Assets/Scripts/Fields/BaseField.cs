using System;
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

    private int[] diceValues;
    private bool isScribbleModeOn;

    #endregion

    #region Protected Members

    protected bool isFilled;
    protected bool isFillable;
    protected bool isCalledRoundInProgress;
    protected bool canScribble;
    protected int roll;

    #endregion

    #region Public Members

    public GameState GameState;
    public RowType Row;
    public IntEvent FieldFilledEvent;

    #endregion

    #region Events

    private void Awake()
    {
        GameState.DiceChangedEvent += DiceChangedHandler;
        GameState.FieldCalledEvent += FieldCalledEventHandler;
        GameState.RollResetEvent += RollResetEventHandler;
        GameState.ScribbleButtonToggledEvent += ScribbleButtonToggledEventHandler;
    }

    private void UnsubscribeFromEvents()
    {
        GameState.DiceChangedEvent -= DiceChangedHandler;
        GameState.FieldCalledEvent -= FieldCalledEventHandler;
        GameState.RollResetEvent -= RollResetEventHandler;
        GameState.ScribbleButtonToggledEvent -= ScribbleButtonToggledEventHandler;
    }

    private void ScribbleButtonToggledEventHandler(bool isOn)
    {
        isScribbleModeOn = isOn;
    }

    private void RollResetEventHandler()
    {
        isCalledRoundInProgress = false;
    }

    private void FieldCalledEventHandler()
    {
        isCalledRoundInProgress = true;
    }

    private void DiceChangedHandler(DiceStruct[] dices)
    {
        ResetButton();

        roll = GameState.Roll;

        if (dices.Length > 0)
            diceValues = dices.Select(x => x.DiceValue).OrderByDescending(x => x).ToArray();
        else
            diceValues = new int[6];

        if (isScribbleModeOn)
            ScribbleHighlightLogic();
        else
            HighlightLogic(dices);

        if (roll == 1)
        {
            if (CanScribble(2))
                GameState.IncrementUnlockCounter();
        }
        else
        {
            GameState.IncrementUnlockCounter();
        }
    }

    #endregion

    #region Fill Logic

    public void FillField()
    {
        if (canScribble)
        {
            ScribbleLogic();
        }
        else
        {
            if (!isFilled)
                FillLogic();
        }
    }

    protected virtual void FillLogic()
    {
        if (isFillable)
            ValueFill(GetFieldValue());
    }

    protected void ValueFill(int fillValue)
    {
        gameObject.GetComponentInChildren<Text>().text = fillValue == 0 ? "/" : fillValue.ToString();

        isFilled = true;

        FieldFilledEvent.Raise(fillValue);

        GameState.Roll = 0;

        ResetButton();

        UnsubscribeFromEvents();
    }

    private int GetDiceSum()
    {
        if (diceValues.Length > 0)
             return diceValues.Sum();

        return 0;
    }

    private int GetKentaValue()
    {
        if (roll == 1)
            return 66;

        if (roll == 2)
            return 56;

        if (roll > 2)
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

    #region Scribble Logic

    private void ScribbleLogic()
    {
        ValueFill(0);
    }

    #endregion

    #region Highlight Logic

    private void HighlightFillable()
    {
        gameObject.GetComponent<Image>().color = Color.cyan;
    }

    protected virtual void HighlightLogic(DiceStruct[] dices)
    {
        IsFillable();

        if (isFillable)
            HighlightFillable();

    }

    private bool AreFieldFillConditionsMet(Func<bool> rowSpecificPredicate)
    {
        if (isCalledRoundInProgress)
            return false;

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

    private void IsFillable()
    {
        isFillable = false;

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

        if (diceValues.Length != 5)
            return false;

        for (int i = diceValues.Length - 1; i > 0; i--)
        {
            if (diceValues[i] != diceValues[i - 1] - 1)
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

    #region Scribble Highlight Logic

    private void ScribbleHighlightLogic()
    {
        if (CanScribble(roll))
        {
            HighlightScribbleField();
            canScribble = true;
        }
    }

    protected virtual bool CanScribble(int rollNumber) => rollNumber > 0 && !isCalledRoundInProgress ? true : false;

    private void HighlightScribbleField()
    {
        gameObject.GetComponent<Image>().color = Color.red;
    }

    #endregion

    #region Reset Field
    private void ResetButton()
    {
        isFillable = false;
        canScribble = false;
        diceValues = new int[6];
        gameObject.GetComponent<Image>().color = Color.white;
    }

    #endregion

    #region Abstract Methods

    protected abstract bool IsColumnSpecificConditionMet();

    #endregion

}
