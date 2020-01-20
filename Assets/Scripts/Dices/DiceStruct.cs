using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DiceStruct
{
    public DiceStruct(DiceId diceId, int diceValue, bool isLocked, int roll)
    {
        DiceId = diceId;
        DiceValue = diceValue;
        IsLocked = isLocked;
    }

    public DiceId DiceId;
    public int DiceValue;
    public bool IsLocked;
}
