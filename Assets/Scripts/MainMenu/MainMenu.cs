using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider LoadingBar;
    public TextMeshProUGUI PercentageLabel;

    private void Awake()
    {
        gameObject.transform.position = Vector2.zero;
        LoadingBar.gameObject.transform.position = new Vector2(9999, 9999);
    }

    public void PlaySinglePlayerGame()
    {
        StartCoroutine(LoadSpecifiedSceneAsync("SinglePlayerGame"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadSpecifiedSceneAsync(string sceneName)
    {
        gameObject.transform.position = new Vector2(9999, 9999);
        LoadingBar.gameObject.transform.position = Vector2.zero;

        var operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBar.value = progress;
            PercentageLabel.text = $"{ (int)(progress * 100) }%";

            yield return null;
        }
    }
}
