using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationsSums : MonoBehaviour
{

    #region Private Members

    private int _kenta;
    private int _trilling;
    private int _fullHouse;
    private int _poker;
    private int _jamb;

    #endregion

    #region Properties

    public int Kenta
    {
        get => _kenta;
        set
        {
            _kenta = value;
            HandleChange();
        }
    }

    public int Trilling
    {
        get => _trilling;
        set
        {
            _trilling = value;
            HandleChange();
        }
    }

    public int FullHouse
    {
        get => _fullHouse;
        set
        {
            _fullHouse = value;
            HandleChange();
        }
    }

    public int Poker
    {
        get => _poker;
        set
        {
            _poker = value;
            HandleChange();
        }
    }

    public int Jamb
    {
        get => _jamb;
        set
        {
            _jamb = value;
            HandleChange();
        }
    }

    #endregion

    #region Public Members

    public IntEvent KentaChangedEvent;
    public IntEvent TrillingChangedEvent;
    public IntEvent FullHouseChangedEvent;
    public IntEvent PokerChangedEvent;
    public IntEvent JambChangedEvent;

    public IntEvent CombinationsSumChangedEvent;

    public Text Text;

    #endregion

    #region Events

    private void OnEnable()
    {
        KentaChangedEvent.EventListeners += HandleKentaChangedEvent;
        TrillingChangedEvent.EventListeners += HandleTrillingChangedEvent;
        FullHouseChangedEvent.EventListeners += HandleFullHouseChangedEvent;
        PokerChangedEvent.EventListeners += HandlePokerChangedEvent;
        JambChangedEvent.EventListeners += HandleJambChangedEvent;
    }

    private void OnDisable()
    {
        KentaChangedEvent.EventListeners -= HandleKentaChangedEvent;
        TrillingChangedEvent.EventListeners -= HandleTrillingChangedEvent;
        FullHouseChangedEvent.EventListeners -= HandleFullHouseChangedEvent;
        PokerChangedEvent.EventListeners -= HandlePokerChangedEvent;
        JambChangedEvent.EventListeners -= HandleJambChangedEvent;
    }

    private void HandleKentaChangedEvent(int value)
    {
        Kenta = value;
    }

    private void HandleTrillingChangedEvent(int value)
    {
        Trilling = value;
    }

    private void HandleFullHouseChangedEvent(int value)
    {
        FullHouse = value;
    }

    private void HandlePokerChangedEvent(int value)
    {
        Poker = value;
    }

    private void HandleJambChangedEvent(int value)
    {
        Jamb = value;
    }

    #endregion

    #region Methods

    private void HandleChange()
    {
        int totalSum = GetTotal();

        Text.text = totalSum.ToString();

        CombinationsSumChangedEvent.Raise(totalSum);
    }

    private int GetTotal() => Kenta + Trilling + FullHouse + Poker + Jamb;

    #endregion
}
