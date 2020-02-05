using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    #region Public Members

    public IntEvent GameOverEvent;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI CongratulationsLabel;

    public ScoringTable ScoringTable;

    #endregion

    #region Events

    private void Awake()
    {
        gameObject.transform.localPosition = new Vector3(1111, 0, 0);
    }

    private void OnEnable()
    {
        GameOverEvent.EventListeners += GameOverHandler;
    }

    private void OnDisable()
    {
        GameOverEvent.EventListeners -= GameOverHandler;

    }

    private void GameOverHandler(int result)
    {
        gameObject.transform.localPosition = new Vector3(0, 136, 0);
        HighScoreText.text = result.ToString();

        if (ScoringTable.GetHighestScore() < result)
        {
            ScoringTable.AddHighScore(result);
            CongratulationsLabel.text = "New Highscore! Congratulations!";
        }
        else
        {
            CongratulationsLabel.text = "";
        }
    }

    #endregion
}
