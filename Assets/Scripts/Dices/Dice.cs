using ScriptableObjectEvents;
using System.Threading;
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
    public Sprite[] DiceSides;

    private DiceStruct dice;
    private Image image;

    private void Awake()
    {
        dice.DiceId = diceId;
        GameState.RollResetEvent += ResetDice;
        image = gameObject.GetComponent<Image>();
    }

    private void OnDisable()
    {
        GameState.RollResetEvent -= ResetDice;
    }

    public void ToggleLock()
    {
        if (GameState.Roll > 0 && GameState.IsRollFinished)
        {
            dice.IsLocked = !dice.IsLocked;
            ChangeHighlight();
            GameState.ModifyDice(dice);
        }
    }

    private void ChangeHighlight()
    {
        var image = gameObject.GetComponent<Image>();

        var color = image.color;

        if (dice.IsLocked)
        {
            color.a = 0.5f;
            image.color = color;
        }
        else
        {
            color.a = 1.0f;
            image.color = color;
        }
    }

    private async Task DiceRollStart()
    {
        int randIndex = 0;

        GameState.IsRollFinished = false;

        int rollCounter = Random.Range(1, 7);

        for (int i = 0; i < rollCounter; i++)
        {
            randIndex = Random.Range(0, 6);
            SetSprite(randIndex);
            await Task.Delay(150);
        }

        dice.DiceValue = randIndex + 1;
    }

    private void DiceRollFinish()
    {
        GameState.SetRollFinished(dice);
        GameState.ModifyDice(dice);
    }

    public override async void HandleEvent()
    {
        if (!dice.IsLocked)
        {
            await DiceRollStart();
        }

        DiceRollFinish();
    }

    private void SetSprite(int spriteIndex)
    {
        image.sprite = DiceSides[spriteIndex];
    }

    private void ResetDice()
    {
        dice.IsLocked = false;
        dice.DiceValue = 0;

        var color = image.color;
        color.a = 1.0f;
        image.color = color;
    }
}
