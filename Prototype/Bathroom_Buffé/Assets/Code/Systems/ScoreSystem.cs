using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private ScoreVariable[] scores = new ScoreVariable[2];

//     public int GetScoreForPlayerIndex(int playerIndex)
//     {
//         return scores[playerIndex].score;
//     }

    public void AddScoreVariable(int playerIndex)
    {
        Debug.Log("AddScoreVariable");
        //scores =  new List<ScoreVariable>();
        ScoreVariable newScoreVariable = new ScoreVariable();
        //newScoreVariable.playerIndex = playerIndex;
        //newScoreVariable.score = 0;
        scores[playerIndex] = newScoreVariable;
    }

    [System.Serializable]
    public struct ScoreVariable
    {
        public int playerIndex;
        public int score;

        public void IncreaseScore(int amount)
        {
            Debug.Log("Score before: " + score + " add amount: " + amount);
            score += amount;
            Debug.Log("Total score after add: " + score);
        }
    }

    public void IncreaseScore(int playerIndex, int scoreToAdd)
    {
        //Debug.Log(scores[playerIndex].score);
        scores[playerIndex].IncreaseScore(scoreToAdd);
    }
}
