using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState GameState;

    private void Awake()
    {
        GameState.Roll = 0;
    }
}
