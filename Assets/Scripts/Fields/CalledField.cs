using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalledField : BaseField
{
    private bool isCalled;

    protected override bool IsColumnSpecificConditionMet()
    {
        return isCalled;
    }

    protected override void FillLogic()
    {
        if (CanCallField())
        {
            CallFill();
        }
        else if (roll == 3 && isFillable == false && isCalled)
        {
            ScribbleLogic();
        }
        else
        {
            base.FillLogic();
        }

    }

    private void CallFill()
    {
        isCalled = true;
        HighlightCallable();
        GameState.RaiseFieldCalledEvent();
    }

    private bool CanCallField() => roll == 1 && !isFilled && !isCalledRoundInProgress && !isCalled;

    protected override void HighlightLogic(DiceStruct[] dices)
    {
        isCalledRoundInProgress = isCalled ? false : isCalledRoundInProgress;

        if (CanCallField())
        {
            HighlightCallable();
        }
        else if (isCalled)
        {
            base.HighlightLogic(dices);

            if (roll == 3 && isFillable == false)
                HighlightScribbleField();
            else if (roll < 3 && isFillable == false)
                HighlightCallable();
        }
        else
        {
            base.HighlightLogic(dices);
        }
    }

    private void HighlightCallable()
    {
        //highlightParticle.SetHighlightColor(HighlightColor.Called);
        highlightParticle.CreateHighlightParticles(HighlightColor.Called);
    }

    protected override bool CanScribble(int rollNumber) => rollNumber > 0 && isCalled;
}
