using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private ScoreVariable[] scores = new ScoreVariable[2];

    public int GetScoreForPlayerIndex(int playerIndex)
    {
        return scores[playerIndex].score;
    }

    public void AddScoreVariable(int playerIndex)
    {
        ScoreVariable newScoreVariable = new ScoreVariable();
        scores[playerIndex] = newScoreVariable;
    }

    [System.Serializable]
    public struct ScoreVariable
    {
        public int playerIndex;
        public int score;

        public void IncreaseScore(int amount)
        {
            score += amount;
        }
    }

    public void IncreaseScore(int playerIndex, int scoreToAdd)
    {
        scores[playerIndex].IncreaseScore(scoreToAdd);
    }
}
