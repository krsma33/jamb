using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BaseField : MonoBehaviour
{
    public GameState GameState;

    private bool isFilled;
    private bool isFillable;

    private void Awake()
    {
        GameState.RollFinishedEvent += RollFinishedHandler;
    }

    private void RollFinishedHandler(DiceStruct[] dices, int roll)
    {

        string diceComb = dices.Select(x => x.DiceValue.ToString()).Aggregate((x, y) => $"{x},{y}");

        Debug.Log(diceComb + ". Roll No: " + roll);

        gameObject.GetComponent<Image>().color = Color.cyan;
    }
}
