using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpField : BaseField
{
    private bool isPreviousFieldFilled;

    public IntEvent PreviousFilledEvent;

    protected override bool IsColumnSpecificConditionMet() => roll > 0 && isPreviousFieldFilled && !isCalledRoundInProgress ? true : false;

    protected override bool CanScribble(int rollNumber) => rollNumber > 0 && isPreviousFieldFilled && !isCalledRoundInProgress ? true : false;

    private void OnEnable()
    {
        if (Row == RowType.Jamb)
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
