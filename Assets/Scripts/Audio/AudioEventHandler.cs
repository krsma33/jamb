using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventHandler : MonoBehaviour
{
    public AudioManager AudioManager;

    public VoidEvent FieldFillSuccess;
    public VoidEvent FieldFillScratch;
    public VoidEvent DiceRolled;

    private void Start()
    {
        AudioManager.PlayMusic("MainMusicTheme");
    }

    private void OnEnable()
    {
        FieldFillSuccess.EventListeners += PlayFilledSound;
        FieldFillScratch.EventListeners += PlayScratchSound;
        DiceRolled.EventListeners += PlayRollDiceSound;
    }

    private void OnDisable()
    {
        FieldFillSuccess.EventListeners -= PlayFilledSound;
        FieldFillScratch.EventListeners -= PlayScratchSound;
        DiceRolled.EventListeners -= PlayRollDiceSound;
    }

    private void PlayScratchSound()
    {
        int randomSfx = Random.Range(1, 4);
        AudioManager.PlaySfx($"FieldScratched{ randomSfx }");
    }

    private void PlayFilledSound()
    {
        int randomSfx = Random.Range(1, 4);
        AudioManager.PlaySfx($"FieldFilled{ randomSfx }");
    }

    private void PlayRollDiceSound()
    {
        int randomSfx = Random.Range(1, 4);
        AudioManager.PlaySfx($"RollDice{ randomSfx }");
    }

}
