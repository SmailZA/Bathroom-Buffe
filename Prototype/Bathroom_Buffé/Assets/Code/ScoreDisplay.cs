using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreTXT;
    public float travelSpeed = 20;
    public float displayDuration = 1.5f;

    float currentTime = 0;

    private void Awake()
    {
        scoreTXT = GetComponent<Text>();
    }

    public void SetScoreText(int score)
    {
        scoreTXT.text = score.ToString();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > displayDuration)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 newPosition = new Vector3(0f, travelSpeed * Time.deltaTime);
        transform.position += newPosition;
    }
}
