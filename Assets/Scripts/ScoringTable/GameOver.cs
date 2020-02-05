using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private bool _gameOver = false;
    private int _consecutiveBackButtonPresses = 0;

    #region Public Members

    public IntEvent GameOverEvent;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI CongratulationsLabel;

    public ScoringTable ScoringTable;

    public GameState GameState;

    #endregion

    #region Events

    private void Awake()
    {
        gameObject.transform.localPosition = new Vector3(1111, 0, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackMainMenu();
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
        _gameOver = true;

        GameState.GameOver();

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

    #region Methods

    public void BackMainMenu()
    {
        StartCoroutine(ResetConsecutiveButtonPressesAfter(1));

        if (_gameOver)
        {
            LoadMainMenuScene();
        }
        else if (_consecutiveBackButtonPresses >= 2)
        {
            LoadMainMenuScene();
        }
    }

    private IEnumerator ResetConsecutiveButtonPressesAfter(float seconds)
    {
        _consecutiveBackButtonPresses++;
        yield return StartCoroutine(Delay(seconds));
        _consecutiveBackButtonPresses = 0;
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    private void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    #endregion
}
