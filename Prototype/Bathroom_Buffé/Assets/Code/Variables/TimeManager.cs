using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{

    float timeStart = 0f;
    public float timeTilEnd = 1f;


    public void Start()
    {
        timeStart = Time.time;
        Invoke("EndGame", timeTilEnd);
    }

    private void EndGame()
    {

    }



}
