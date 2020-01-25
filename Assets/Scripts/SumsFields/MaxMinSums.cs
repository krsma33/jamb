using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxMinSums : MonoBehaviour
{

    #region Private Members

    private int _one;
    private int _max;
    private int _min;

    #endregion

    #region Properties

    public int One
    {
        get => _one;
        set
        {
            _one = value;
            HandleChange();
        }
    }

    public int Max
    {
        get => _max;
        set
        {
            _max = value;
            HandleChange();
        }
    }

    public int Min
    {
        get => _min;
        set
        {
            _min = value;
            HandleChange();
        }
    }

    #endregion

    #region Public Members

    public IntEvent OneChangedEvent;
    public IntEvent MaxChangedEvent;
    public IntEvent MinChangedEvent;

    public IntEvent MinMaxSumChangedEvent;

    public Text Text;

    #endregion

    #region Events

    private void OnEnable()
    {
        OneChangedEvent.EventListeners += HandleOneChangedEvent;
        MaxChangedEvent.EventListeners += HandleMaxChangedEvent;
        MinChangedEvent.EventListeners += HandleMinChangedEvent;
    }

    private void OnDisable()
    {
        OneChangedEvent.EventListeners -= HandleOneChangedEvent;
        MaxChangedEvent.EventListeners -= HandleMaxChangedEvent;
        MinChangedEvent.EventListeners -= HandleMinChangedEvent;
    }

    private void HandleOneChangedEvent(int value)
    {
        One = value;
    }

    private void HandleMaxChangedEvent(int value)
    {
        Max = value;
    }

    private void HandleMinChangedEvent(int value)
    {
        Min = value;
    }

    #endregion

    #region Methods

    private void HandleChange()
    {
        int totalSum = SumLogic();

        Text.text = totalSum.ToString();

        MinMaxSumChangedEvent.Raise(totalSum);
    }

    private int SumLogic()
    {
        if (Max == 0 || Min == 0)
            return 0;

        if (Max > Min)
            return GetTotal();

        return 0;
    }

    private int GetTotal() => (Max - Min) * One;

    #endregion
}
