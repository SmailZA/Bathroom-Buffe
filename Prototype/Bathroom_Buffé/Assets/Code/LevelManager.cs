using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Tooltip("Each element contains how many bubbles you need to popp to progress.")]
    public int[] bubblesPoppedPerLevel;
    public Text timerLabel;

    public BubbleSpawner bubbleSpawner;

    public Text currentLevelText;

    public float timeUntilLevelStart = 3f;

    private int currentLevel = 0;
    private int actuallyCurrentLevel = 1;
    public static int currentBubblesPopped = 0;
    private int bubblesPerLvl = 10;
    private float timeStart = 0f;
    public float timeTilEnd = 40f;
    private float CurrentMaxTime = 0;
    private bool CanPlay
    { get; set; }

    private float levelStartTime;

   static int totalScore = 0;


    private void Start()
    {
        timeTilEnd = 60;
        CurrentMaxTime = timeTilEnd;
        StartCoroutine(CountDownTimer());
        currentBubblesPopped = 0;
        totalScore = 0;
        timeStart = Time.time;
        Invoke("EndGame", timeTilEnd);
        InvokeRepeating("CountDown", 0, 1f);
        InvokeRepeating("BubbleRemain", 0, 0.1f);
    }
    void BubbleRemain()
    {
        this.transform.parent.transform.GetChild(2).gameObject.GetComponent<Text>().text = (bubblesPerLvl - currentBubblesPopped).ToString();

    }
    void CountDown()
    {

        timeTilEnd--;
        this.GetComponent<Text>().text = timeTilEnd.ToString(); 

    }
    private IEnumerator CountDownTimer()
    {
        StopPlay();

        float currentTime = timeUntilLevelStart;
        while (currentTime >= 0f)
        {
            this.transform.parent.transform.GetChild(1).gameObject.GetComponent<Text>().text = currentTime.ToString();

            yield return new WaitForSecondsRealtime(1f);
            currentTime -= 1f;
        }
        StartPlay();
        yield return null;
    }


    private void StopPlay()
    {
        CanPlay = false;
        this.transform.parent.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = true;
        this.transform.parent.transform.GetChild(1).gameObject.GetComponent<Text>().enabled = true;
        Time.timeScale = 0f;

    }
    private void StartPlay()
    {
        timeTilEnd = CurrentMaxTime;
        CanPlay = true;
        this.transform.parent.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
        //timerLabel.gameObject.SetActive(false);
        this.transform.parent.transform.GetChild(1).gameObject.GetComponent<Text>().enabled = false;
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

    public void LevelCountDownTimer()
    {
        timeStart += Time.deltaTime;
        //Debug.Log(timeStart);
        if(timeStart == timeTilEnd)
        {
            //create new scene
        }

    }

    public static void AddToScore(int scorePoint)
    {
        totalScore += scorePoint;
    }

    private void EndGame()
    {
        SceneManager.LoadScene("GameOver");

    }

    //[SerializeField] private float maxLevelLifeTime = 60f;

    //private float levelTimeElapsed = 0;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    void Update()
   {
        LevelCountDownTimer();
        Debug.Log(currentBubblesPopped);
        if(currentBubblesPopped >= bubblesPerLvl)
        {
            timeTilEnd = CurrentMaxTime + 10;
            CurrentMaxTime += 10;
            bubblesPerLvl += 10;
            currentBubblesPopped = 0;
            actuallyCurrentLevel++;
            currentLevelText.text = actuallyCurrentLevel.ToString(); 
            if (bubbleSpawner)
            {
                bubbleSpawner.spawnInterval -= 0.1f;
                bubbleSpawner.spawnInterval = Mathf.Clamp(bubbleSpawner.spawnInterval, 0.2f, 1);
            }
            else
            {
                Debug.Log("You have encountered a grotesque error! Please assign BubbleSpawner to field in this object");
            }
            CancelInvoke("EndGame");
            Invoke("EndGame", timeTilEnd);
            StartCoroutine(CountDownTimer());
        }
    //    levelTimeElapsed += Time.deltaTime;

    //    if(levelTimeElapsed == maxLevelLifeTime)
    //    {
    //        //GameOver;
    //    }

    }
}
