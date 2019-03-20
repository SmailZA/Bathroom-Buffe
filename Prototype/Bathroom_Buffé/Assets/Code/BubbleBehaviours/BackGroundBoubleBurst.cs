using UnityEngine;

public class BackGroundBoubleBurst : MonoBehaviour
{
    int Timer;
    int MaxTimer = 1000;

    // Update is called once per frame
    void Start()
    {
        Timer = Random.Range(400, MaxTimer - 100);
    }


    void Update()
    {
        if (Timer > MaxTimer)
        {
            GetComponent<Animator>().SetBool("PlayBubbleAnimation", true);
            RandomTimer();
        }
        else
        {
            Timer++;
            GetComponent<Animator>().SetBool("PlayBubbleAnimation", false);
        }
           

    }

    void RandomTimer ()
    {
        float tempScale = Random.Range(0.3f, 1.2f);
        this.transform.localScale = new Vector3(tempScale, tempScale, tempScale);
        Timer = Random.Range(1 , MaxTimer-100);
    }
}
