using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bubble : MonoBehaviour
{
    public BubbleType type;

    Rigidbody2D body;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(BubbleType type)
    {
        this.type = type;
        spriteRenderer.sprite = type.sprite;
    }

    private void Update()
    {
        Travel();
    }

    void Travel()
    {
        /* Cast transform.up to Vector2 because it is a Vector3, Z value is not needed & removed */
        Vector2 newPosition = (Vector2)transform.up * type.speed;
        body.AddForce(newPosition);
        //body.MovePosition(newPosition * Time.deltaTime);
    }
}
