using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public ScoringTable HighScores;

    public TextMeshProUGUI[] HighScoreLabels;

    private void Awake()
    {
        transform.position = new Vector2(2222, 0);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PressBack();
    }

    public void LoadHighScores()
    {
        var topScores = HighScores.GetTopTenHighScores();
        int topScoresCount = topScores.Length;

        for (int i = 0; i < HighScoreLabels.Length; i++)
        {
            string highScoreText = "0";

            if (i < topScoresCount)
                highScoreText = topScores[i].ToString();

            HighScoreLabels[i].text = highScoreText;
        }
    }

    public void PressBack()
    {
        transform.position = new Vector2(2222, 0);
        MainMenu.transform.position = Vector2.zero;
    }
}
