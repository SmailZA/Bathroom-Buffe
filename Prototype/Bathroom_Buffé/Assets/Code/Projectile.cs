using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    Rigidbody2D body;

    public float launchSpeed = 15f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Launch()
    {
        Vector2 forceDirection = (transform.up * launchSpeed);

        // I've heard rumors that setting velocity directly is not good, I don't care
        body.velocity = forceDirection;
    }

    public void Tick()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered: " + collision.name);
        Destroy(gameObject);
    }
}
