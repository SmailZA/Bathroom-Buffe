using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BubbleSpawner : MonoBehaviour
{
    BoxCollider2D boxCollider2D;

    public GameObject bubblePrefab;
    public List<BubbleType> bubbles = new List<BubbleType>();

    public float spawnInterval = 1f;

    float spawnTimer = 0f;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void Tick()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnInterval)
        {
            SpawnBubble();
            spawnTimer = 0f;
        }
    }

    public void SpawnBubble()
    {
        float randX = Random.Range(boxCollider2D.bounds.min.x, boxCollider2D.bounds.max.x);
        float randY = Random.Range(boxCollider2D.bounds.min.y, boxCollider2D.bounds.max.y);
        Vector2 randomPosition = new Vector2(randX, randY);

        GameObject newBubbleGO = Instantiate(bubblePrefab, randomPosition, Quaternion.identity);
        Bubble newBubble = newBubbleGO.GetComponent<Bubble>();
        newBubble.Initialize(bubbles[Random.Range(0, bubbles.Count)]);
    }
}
