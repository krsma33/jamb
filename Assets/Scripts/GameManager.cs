using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState GameState;

    private void Awake()
    {
        GameState.DicesSetCounter = 0;
        GameState.Roll = 0;
    }
}
