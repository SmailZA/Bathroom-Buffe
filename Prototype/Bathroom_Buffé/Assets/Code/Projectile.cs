using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    Rigidbody2D body;

    [HideInInspector] public FlyController controller;

    public float launchSpeed = 15f;

    public delegate void OnDestroyProjectileDelegate(Projectile projectile);
    public OnDestroyProjectileDelegate OnDestroyProjectile;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Launch(FlyController flyController)
    {
        controller = flyController;
        Vector2 forceDirection = (transform.up * launchSpeed);

        // I've heard rumors that setting velocity directly is not good, I don't care, maybe you should
        body.velocity = forceDirection;
    }

    public void Tick()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If collided object is on "Water" layer
        if(collision.gameObject.layer == 10)
        {
            OnDestroyProjectile?.Invoke(this);
            return;
        }

        if (collision.GetComponent<Projectile>())
        {
            return;
        }

        if (collision.gameObject != this)
        {
            Bubble collidedBubble = collision.GetComponent<Bubble>();
            BubbleBehaviour collidedBehaviour;
            if (collidedBubble)
            {
                collidedBehaviour = collidedBubble.type.bubbleBehaviour;
                collidedBehaviour?.OnShot(collision.gameObject, gameObject);
            }

            FlyController flyController = collision.GetComponent<FlyController>();
        }
        // Makes projectile system remove this from it's projectile list & destroys this
        OnDestroyProjectile?.Invoke(this);
    }
}
