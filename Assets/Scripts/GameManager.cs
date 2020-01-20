using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private DiceStruct DiceOne;
    private DiceStruct DiceTwo;
    private DiceStruct DiceThree;
    private DiceStruct DiceFour;
    private DiceStruct DiceFive;
    private DiceStruct DiceSix;

    public void SetDiceOne(DiceStruct diceStruct)
    {
        DiceOne = diceStruct;
        Debug.Log($"Dice struct { diceStruct.DiceId} set with value { diceStruct.DiceValue }, and is locked = { diceStruct.DiceLocked }");
    }

    public void SetDiceTwo(DiceStruct diceStruct)
    {
        DiceTwo = diceStruct;
        Debug.Log($"Dice struct { diceStruct.DiceId} set with value { diceStruct.DiceValue }, and is locked = { diceStruct.DiceLocked }");
    }

    public void SetDiceThree(DiceStruct diceStruct)
    {
        DiceThree = diceStruct;
        Debug.Log($"Dice struct { diceStruct.DiceId} set with value { diceStruct.DiceValue }, and is locked = { diceStruct.DiceLocked }");
    }

    public void SetDiceFour(DiceStruct diceStruct)
    {
        DiceFour = diceStruct;
        Debug.Log($"Dice struct { diceStruct.DiceId} set with value { diceStruct.DiceValue }, and is locked = { diceStruct.DiceLocked }");
    }

    public void SetDiceFive(DiceStruct diceStruct)
    {
        DiceFive = diceStruct;
        Debug.Log($"Dice struct { diceStruct.DiceId} set with value { diceStruct.DiceValue }, and is locked = { diceStruct.DiceLocked }");
    }

    public void SetDiceSix(DiceStruct diceStruct)
    {
        DiceSix = diceStruct;
        Debug.Log($"Dice struct { diceStruct.DiceId} set with value { diceStruct.DiceValue }, and is locked = { diceStruct.DiceLocked }");
    }
}
