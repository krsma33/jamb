using ScriptableObjectEvents;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public enum DiceId
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six
}

public class Dice : GameEventListener<VoidEvent>
{
    public DiceId diceId;
    public GameState GameState;

    private DiceStruct dice;

    private void Awake()
    {
        dice.DiceId = diceId;
    }

    public void ToggleLock()
    {
        dice.IsLocked = !dice.IsLocked;
    }

    private void DiceRollStart()
    {
        dice.DiceValue = Random.Range(1, 7);
    }

    private int DiceRollRandomDelay() => Random.Range(200, 2200);

    private void DiceRollFinish()
    {
        GameState.AddOrModifyDice(dice);
    }

    public override async void HandleEvent()
    {
        DiceRollStart();
        await Task.Delay(DiceRollRandomDelay());
        DiceRollFinish();
    }
}
