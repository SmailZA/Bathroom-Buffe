using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFog : MonoBehaviour
{
    public float min = 1f;
    public float max = 2f;

    void Start()
    {
        min = transform.position.x;
        max = transform.position.x + 2;
    }
    private void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * 1, max - min) + min, transform.position.y);

       
    }
}
