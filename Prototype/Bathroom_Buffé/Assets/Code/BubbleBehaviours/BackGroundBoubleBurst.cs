using UnityEngine;

public class BackGroundBoubleBurst : MonoBehaviour
{
    public int Timer = 1;
    public int MaxTimer = 300;

    // Update is called once per frame
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
