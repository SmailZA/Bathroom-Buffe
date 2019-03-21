using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public RectTransform[] scoreGOs = new RectTransform[4];

    private ScoreVariable[] scores = new ScoreVariable[4];

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

    public void AddNewPlayerScore(int index)
    {
        AddScoreVariable(index);
        //scoreGOs[index].GetComponent<Text>().text = scores[index].ToString();
        scoreGOs[index].GetComponent<Text>().text = scores[index].score.ToString();
        foreach (Transform go in scoreGOs[index].GetComponentsInChildren<Transform>())
        {
            Debug.Log("found go: " + go + " saved gameobject for num: " + scoreGOs[index]);
            if (go.name != scoreGOs[index].name)
                go.gameObject.SetActive(false);
        }
    }

    public void IncreaseScore(int playerIndex, int scoreToAdd)
    {
        scores[playerIndex].IncreaseScore(scoreToAdd);
        scoreGOs[playerIndex].GetComponent<Text>().text = scores[playerIndex].score.ToString();
    }
}
