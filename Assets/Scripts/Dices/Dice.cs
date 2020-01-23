using ScriptableObjectEvents;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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
        GameState.RollResetEvent += ResetDice;
    }

    private void OnDisable()
    {
        GameState.RollResetEvent -= ResetDice;
    }

    public void ToggleLock()
    {
        if (GameState.Roll > 0 && GameState.AllDiceSet)
        {
            dice.IsLocked = !dice.IsLocked;
            ChangeHighlight();
            GameState.ModifyDice(dice);
        }
    }

    private void ChangeHighlight()
    {
        var image = gameObject.GetComponent<Image>();

        if (dice.IsLocked)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.white;
        }
    }

    private void DiceRollStart()
    {
        dice.DiceValue = Random.Range(1, 7);
    }

    private int DiceRollRandomDelay() => Random.Range(200, 2000);

    private void DiceRollFinish()
    {
        gameObject.GetComponentInChildren<Text>().text = dice.DiceValue.ToString();

        GameState.SetRollFinished(dice);
        GameState.ModifyDice(dice);
    }

    public override async void HandleEvent()
    {
        if (!dice.IsLocked)
        {
            DiceRollStart();
            await Task.Delay(DiceRollRandomDelay());
        }

        DiceRollFinish();
    }

    private void ResetDice()
    {
        dice.IsLocked = false;
        dice.DiceValue = 0;
    }
}
