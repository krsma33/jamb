using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSums : MonoBehaviour
{

    #region Private Members

    private int _one;
    private int _two;
    private int _three;
    private int _four;
    private int _five;
    private int _six;

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

    public int Two
    {
        get => _two;
        set
        {
            _two = value;
            HandleChange();
        }
    }

    public int Three
    {
        get => _three;
        set
        {
            _three = value;
            HandleChange();
        }
    }

    public int Four
    {
        get => _four;
        set
        {
            _four = value;
            HandleChange();
        }
    }

    public int Five
    {
        get => _five;
        set
        {
            _five = value;
            HandleChange();
        }
    }

    public int Six
    {
        get => _six;
        set
        {
            _six = value;
            HandleChange();
        }
    }

    #endregion

    #region Public Members

    public IntEvent OneChangedEvent;
    public IntEvent TwoChangedEvent;
    public IntEvent ThreeChangedEvent;
    public IntEvent FourChangedEvent;
    public IntEvent FiveChangedEvent;
    public IntEvent SixChangedEvent;

    public IntEvent NumberSumChangedEvent;

    public Text Text;

    #endregion

    #region Events

    private void OnEnable()
    {
        OneChangedEvent.EventListeners += HandleOneChangedEvent;
        TwoChangedEvent.EventListeners += HandleTwoChangedEvent;
        ThreeChangedEvent.EventListeners += HandleThreeChangedEvent;
        FourChangedEvent.EventListeners += HandleFourChangedEvent;
        FiveChangedEvent.EventListeners += HandleFiveChangedEvent;
        SixChangedEvent.EventListeners += HandleSixChangedEvent;
    }

    private void OnDisable()
    {
        OneChangedEvent.EventListeners -= HandleOneChangedEvent;
        TwoChangedEvent.EventListeners -= HandleTwoChangedEvent;
        ThreeChangedEvent.EventListeners -= HandleThreeChangedEvent;
        FourChangedEvent.EventListeners -= HandleFourChangedEvent;
        FiveChangedEvent.EventListeners -= HandleFiveChangedEvent;
        SixChangedEvent.EventListeners -= HandleSixChangedEvent;
    }

    private void HandleOneChangedEvent(int value)
    {
        One = value;
    }

    private void HandleTwoChangedEvent(int value)
    {
        Two = value;
    }                  
                       
    private void HandleThreeChangedEvent(int value)
    {
        Three = value;
    }                  
                       
    private void HandleFourChangedEvent(int value)
    {
        Four = value;
    }                  

    private void HandleFiveChangedEvent(int value)
    {
        Five = value;
    }

    private void HandleSixChangedEvent(int value)
    {
        Six = value;
    }

    #endregion

    #region Methods

    private void HandleChange()
    {
        int totalSum = SumLogic();

        Text.text = totalSum.ToString();

        NumberSumChangedEvent.Raise(totalSum);
    }

    private int SumLogic()
    {
        int totalSum = GetTotal();

        return totalSum >= 60 ? totalSum + 30 : totalSum;
    }

    private int GetTotal() => One + Two + Three + Four + Five + Six;

    #endregion

}
