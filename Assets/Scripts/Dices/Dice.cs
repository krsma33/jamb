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

    private void DiceRollStart()
    {
        GameState.IsRollFinished = false;
        dice.DiceValue = Random.Range(1, 7);
    }

    private int DiceRollRandomDelay() => Random.Range(2200, 7700);

    private void DiceRollFinish()
    {
        image.sprite = DiceSides[dice.DiceValue - 1];

        GameState.SetRollFinished(dice);
        GameState.ModifyDice(dice);
    }

    public override async void HandleEvent()
    {
        if (!dice.IsLocked)
        {
            DiceRollStart();

            //var tokenSource = new CancellationTokenSource();
            //var token = tokenSource.Token;
            //tokenSource.CancelAfter(DiceRollRandomDelay());

            //await SimulateRoll(token);
        }

        DiceRollFinish();
    }

    private void SetRandomSprite()
    {
        int randIndex = Random.Range(0, 6);
        image.sprite = DiceSides[randIndex];
    }

    private async Task SimulateRoll(CancellationToken token)
    {
        await Task.Run(async () =>
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();

                SetRandomSprite();
                await Task.Delay(300, token);
            }
        }, token);
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
