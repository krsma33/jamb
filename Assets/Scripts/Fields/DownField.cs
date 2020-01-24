using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownField : BaseField
{
    private bool isPreviousFieldFilled;

    public IntEvent PreviousFilledEvent;

    protected override bool IsColumnSpecificConditionMet() => roll > 0 && isPreviousFieldFilled && !isCalledRoundInProgress ? true : false;

    protected override bool ShouldScribble() => IsColumnSpecificConditionMet();

    private void OnEnable()
    {
        if (Row == RowType.One)
            isPreviousFieldFilled = true;

        if (PreviousFilledEvent is null)
            return;

        PreviousFilledEvent.EventListeners += PreviousFilledEventHandler;
    }

    private void OnDisable()
    {
        if (PreviousFilledEvent is null)
            return;

        PreviousFilledEvent.EventListeners -= PreviousFilledEventHandler;
    }

    private void PreviousFilledEventHandler(int value)
    {
        isPreviousFieldFilled = true;
    }

}
