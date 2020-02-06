using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;

[CreateAssetMenu]
public class ScoringTable : ScriptableObject
{
    private List<int> _highScores { get; set; } = new List<int>();
    private string _persistentObjectPath;

    private void OnEnable()
    {
        _persistentObjectPath = $"{ Application.persistentDataPath }/highscores.json";

        DeserializeHighScores();
    }

    public int GetHighestScore()
    {
        if (_highScores.Count > 0)
            return _highScores[0];

        return 0;
    }

    public void AddHighScore(int highScore)
    {
        _highScores.Add(highScore);
        _highScores.Sort((a, b) => b.CompareTo(a));

        SerializeHighScores();
    }

    public int[] GetTopTenHighScores()
    {
        return _highScores.Take(10).ToArray();
    }

    public void SerializeHighScores()
    {
        string highscoresStringRepresentation = _highScores.Select(x => x.ToString()).Aggregate((x, y) => $"{ x },{ y }");

        File.WriteAllText(_persistentObjectPath, highscoresStringRepresentation);
    }

    public void DeserializeHighScores()
    {
        if (File.Exists(_persistentObjectPath))
        {
            string highscoresStringRepresentation = File.ReadAllText(_persistentObjectPath);

            _highScores = highscoresStringRepresentation.Split(',').Select(x => int.Parse(x)).ToList();
        }
        else
        {
            _highScores = new List<int>();
        }

    }
}
