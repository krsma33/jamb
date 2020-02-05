using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class ScoringTable : ScriptableObject
{
    private List<int> _highScores { get; set; } = new List<int>();

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
    }

    public int[] GetTopTenHighScores()
    {
        return _highScores.Take(10).ToArray();
    }
}
