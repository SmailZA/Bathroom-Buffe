using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public GameObject scoreGO;
    public GameObject canvasGO;

    public RectTransform[] scoreGOs = new RectTransform[4];
    public int fontSize = 26;

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

        public void DecreaseScore(int amount)
        {
            score -= amount;
        }
    }

    public void AddOnScreenScoreMessage(Vector2 screenPosition, int scoreAmount)
    {
        Vector3 pixelPosition = Camera.main.WorldToScreenPoint(screenPosition);

        Debug.Log(pixelPosition);
        GameObject newScore = Instantiate(scoreGO, pixelPosition, Quaternion.identity, canvasGO.transform);
        ScoreDisplay scoreDisplay = newScore.GetComponent<ScoreDisplay>();
        scoreDisplay.SetScoreText(scoreAmount);
    }

    public void AddNewPlayerScore(int index)
    {
        AddScoreVariable(index);
        Text scoreTXT = scoreGOs[index].GetComponent<Text>();
        scoreTXT.text = scores[index].score.ToString();
        scoreTXT.fontSize = fontSize;
        foreach (Transform go in scoreGOs[index].GetComponentsInChildren<Transform>())
        {
/*            Debug.Log("found go: " + go + " saved gameobject for num: " + scoreGOs[index]);*/
            if (go.name != scoreGOs[index].name)
                go.gameObject.SetActive(false);
        }
    }

    public void IncreaseScore(int playerIndex, int scoreToAdd)
    {
        scores[playerIndex].IncreaseScore(scoreToAdd);
        scoreGOs[playerIndex].GetComponent<Text>().text = scores[playerIndex].score.ToString();
    }

    public void DecreaseScore(int playerIndex, int scoreToDecrease)
    {
        scores[playerIndex].DecreaseScore(scoreToDecrease);
        scoreGOs[playerIndex].GetComponent<Text>().text = scores[playerIndex].score.ToString();
    }
}
