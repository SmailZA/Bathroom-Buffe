using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFog : MonoBehaviour
{
    public float min = -5f;
    public float max = -1f;
    public float timeCounter = 1f;

    private void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * timeCounter, max - min) + min, transform.position.y);
    }
}
