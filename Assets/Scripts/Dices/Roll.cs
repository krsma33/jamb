using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
    public GameState GameState;
    public VoidEvent DiceRolled;

    public void RollDice()
    {
        if (GameState.Roll < 3)
        {
            GameState.Roll++;
            DiceRolled.Raise();
        }
    }
}
