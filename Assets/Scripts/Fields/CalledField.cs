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
        else
        {
            base.FillLogic();
        }

    }

    private void CallFill()
    {
        isCalled = true;
        gameObject.GetComponent<Image>().color = Color.green;
    }

    private bool CanCallField() => roll == 1 && !isFilled && !isCalled;

    protected override void HighlightLogic(DiceStruct[] dices)
    {
        if (CanCallField())
        {
            HighlightCallable();
        }
        else
        {
            base.HighlightLogic(dices);
        }
    }

    private void HighlightCallable()
    {
        gameObject.GetComponent<Image>().color = Color.magenta;
    }
}
