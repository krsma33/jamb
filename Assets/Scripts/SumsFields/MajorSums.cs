using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MajorSums : MonoBehaviour
{

    #region Private Members

    private int _down;
    private int _free;
    private int _up;
    private int _called;
    private int _hand;

    #endregion

    #region Properties

    public int Down
    {
        get => _down;
        set
        {
            _down = value;
            HandleChange();
        }
    }

    public int Free
    {
        get => _free;
        set
        {
            _free = value;
            HandleChange();
        }
    }

    public int Up
    {
        get => _up;
        set
        {
            _up = value;
            HandleChange();
        }
    }

    public int Called
    {
        get => _called;
        set
        {
            _called = value;
            HandleChange();
        }
    }

    public int Hand
    {
        get => _hand;
        set
        {
            _hand = value;
            HandleChange();
        }
    }


    #endregion

    #region Public Members

    public IntEvent DownChangedEvent;
    public IntEvent FreeChangedEvent;
    public IntEvent UpChangedEvent;
    public IntEvent CalledChangedEvent;
    public IntEvent HandChangedEvent;

    public IntEvent MajorSumChangedEvent;

    public Text Text;

    #endregion

    #region Events

    private void OnEnable()
    {
        DownChangedEvent.EventListeners += HandleDownChangedEvent;
        FreeChangedEvent.EventListeners += HandleFreeChangedEvent;
        UpChangedEvent.EventListeners += HandleUpChangedEvent;
        CalledChangedEvent.EventListeners += HandleCalledChangedEvent;
        HandChangedEvent.EventListeners += HandleHandChangedEvent;
    }

    private void OnDisable()
    {
        DownChangedEvent.EventListeners -= HandleDownChangedEvent;
        FreeChangedEvent.EventListeners -= HandleFreeChangedEvent;
        UpChangedEvent.EventListeners -= HandleUpChangedEvent;
        CalledChangedEvent.EventListeners -= HandleCalledChangedEvent;
        HandChangedEvent.EventListeners -= HandleHandChangedEvent;
    }

    private void HandleDownChangedEvent(int value)
    {
        Down = value;
    }

    private void HandleFreeChangedEvent(int value)
    {
        Free = value;
    }

    private void HandleUpChangedEvent(int value)
    {
        Up = value;
    }

    private void HandleCalledChangedEvent(int value)
    {
        Called = value;
    }

    private void HandleHandChangedEvent(int value)
    {
        Hand = value;
    }

    #endregion

    #region Methods

    private void HandleChange()
    {
        int totalSum = GetTotal();

        Text.text = totalSum.ToString();

        MajorSumChangedEvent.Raise(totalSum);
    }

    private int GetTotal() => Down + Free + Up + Called + Hand;

    #endregion

}
