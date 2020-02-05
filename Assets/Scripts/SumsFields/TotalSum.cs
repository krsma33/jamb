using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalSum : MonoBehaviour
{

    #region Private Members

    private int _numbersSum;
    private int _maxMinSum;
    private int _combinationsSum;

    private int _filledFieldsCount;

    #endregion

    #region Properties

    public int NumbersSum
    {
        get => _numbersSum;
        set
        {
            _numbersSum = value;
            HandleChange();
        }
    }

    public int MaxMinSum
    {
        get => _maxMinSum;
        set
        {
            _maxMinSum = value;
            HandleChange();
        }
    }

    public int CombinationsSum
    {
        get => _combinationsSum;
        set
        {
            _combinationsSum = value;
            HandleChange();
        }
    }

    #endregion

    #region Public Members

    public IntEvent NumbersSumChangedEvent;
    public IntEvent MaxMinSumChangedEvent;
    public IntEvent CombinationsSumChangedEvent;

    public IntEvent GameOverEvent;

    public VoidEvent FieldFilledEvent;
    public VoidEvent FieldScribbledEvent;

    public Text Text;

    public int TotalFillableFields;

    #endregion

    #region Events

    private void OnEnable()
    {
        NumbersSumChangedEvent.EventListeners += HandleNumbersSumChangedEvent;
        MaxMinSumChangedEvent.EventListeners += HandleMaxMinSumChangedEvent;
        CombinationsSumChangedEvent.EventListeners += HandleCombinationsSumChangedEvent;
        FieldFilledEvent.EventListeners += HandleFieldFilledEvent;
        FieldScribbledEvent.EventListeners += HandleFieldScribbledEvent;
    }

    private void OnDisable()
    {
        NumbersSumChangedEvent.EventListeners -= HandleNumbersSumChangedEvent;
        MaxMinSumChangedEvent.EventListeners -= HandleMaxMinSumChangedEvent;
        CombinationsSumChangedEvent.EventListeners -= HandleCombinationsSumChangedEvent;
        FieldFilledEvent.EventListeners -= HandleFieldFilledEvent;
        FieldScribbledEvent.EventListeners -= HandleFieldScribbledEvent;
    }

    private void HandleNumbersSumChangedEvent(int value)
    {
        NumbersSum = value;
    }

    private void HandleMaxMinSumChangedEvent(int value)
    {
        MaxMinSum = value;
    }

    private void HandleCombinationsSumChangedEvent(int value)
    {
        CombinationsSum = value;
    }

    private void HandleFieldFilledEvent()
    {
        GameEndLogic();
    }

    private void HandleFieldScribbledEvent()
    {
        GameEndLogic();
    }

    #endregion

    #region Methods

    private void GameEndLogic()
    {
        _filledFieldsCount++;

        if (_filledFieldsCount >= TotalFillableFields)
            GameOverEvent.Raise(GetTotal());
    }

    private void HandleChange()
    {
        int totalSum = GetTotal();

        Text.text = totalSum.ToString();
    }

    private int GetTotal() => NumbersSum + MaxMinSum + CombinationsSum;

    #endregion

}
