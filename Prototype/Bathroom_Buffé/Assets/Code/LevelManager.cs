using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Tooltip("Each element contains how many bubbles you need to popp to progress.")]
    public int[] bubblesPoppedPerLevel;
    public Text timerLabel;

    public float timeUntilLevelStart = 3f;

    private int currentLevel = 0;
    private int currentBubblesPopped = 0;

    private bool CanPlay
    { get; set; }

    private float levelStartTime;

   static int totalScore = 0;

    private void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    private IEnumerator CountDownTimer()
    {
        CanPlay = false;
        Time.timeScale = 0f;
        timerLabel.transform.parent.gameObject.SetActive(true);
        float currentTime = timeUntilLevelStart;
        while (currentTime >= 0f)
        {
            timerLabel.text = currentTime.ToString();

            yield return new WaitForSecondsRealtime(1f);
            currentTime -= 1f;
        }
        CanPlay = true;
        timerLabel.transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1f;
        currentBubblesPopped = 0;
    }

    public void OnBubblePopped(Projectile projectile)
    {
        currentBubblesPopped++;
        Debug.Log(currentBubblesPopped + " / " + bubblesPoppedPerLevel[currentLevel]);
        if (currentBubblesPopped >= bubblesPoppedPerLevel[currentLevel])
        {
            currentLevel++;
            if (currentLevel >= bubblesPoppedPerLevel.Length - 1)
            {
               
            }
            else
            {
                StartCoroutine(CountDownTimer());
                // Todo: Kasper let the level manager know a bubble has been popped.
                // currentBubblesPopped++
            }
        }
    }

    public static void AddToScore(int scorePoint)
    {
        totalScore += scorePoint;
    }

    //[SerializeField] private float maxLevelLifeTime = 60f;

    //private float levelTimeElapsed = 0;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    levelTimeElapsed += Time.deltaTime;

    //    if(levelTimeElapsed == maxLevelLifeTime)
    //    {
    //        //GameOver;
    //    }
    //}
}
