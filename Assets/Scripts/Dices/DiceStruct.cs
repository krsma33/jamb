using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DiceStruct
{
    public DiceStruct(int diceId, int diceValue, bool diceLocked)
    {
        DiceId = diceId;
        DiceValue = diceValue;
        DiceLocked = diceLocked;
    }

    public int DiceId;
    public int DiceValue;
    public bool DiceLocked;
}
