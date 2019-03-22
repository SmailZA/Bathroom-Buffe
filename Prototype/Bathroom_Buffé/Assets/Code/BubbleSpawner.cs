using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BubbleSpawner : MonoBehaviour
{
    BoxCollider2D boxCollider2D;



    public bool spawnBubbles = true;

    public GameObject bubblePrefab;
    public List<BubbleType> bubbles = new List<BubbleType>();

    public float spawnInterval = 1f;

    double spawnTimer = 0f;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        faceRenderer = poopFaceRenderGO?.GetComponent<SpriteRenderer>();
        currentPoopBubSpawnInterval = Random.Range(poopBubMinSpawnInterval, poopBubMaxSpawnInterval);
    }

    public void Tick()
    {
        if (!spawnBubbles)
            return;

        spawnTimer += Time.deltaTime;
        currentAnimateTime += Time.deltaTime;
        currentPoopBubSpawnTime += Time.deltaTime;

        if (spawnTimer > spawnInterval)
        {
            SpawnBubble(bubblePrefab, 3);
            spawnTimer = 0f;
        }

        if (currentPoopBubSpawnTime > currentPoopBubSpawnInterval - spawnPoopBubTelegraphTime)
        {
            spawningPoopBub = true;
        }

        if (spawningPoopBub)
        {
            poopFaceRenderGO.SetActive(true);
            defaultFaceRenderGO.SetActive(false);
            AnimatePoopFace();
        }

        if (currentPoopBubSpawnTime > currentPoopBubSpawnInterval)
        {
            SpawnPoopBubble();
        }
    }

    public void SpawnPoopBubble()
    {
        SpawnBubble(poopBubGO, 1);
        spawningPoopBub = false;
        currentPoopBubSpawnInterval = Random.Range(poopBubMinSpawnInterval, poopBubMaxSpawnInterval);
        faceRenderer.sprite = defaultFace;
        defaultFaceRenderGO.SetActive(true);
        poopFaceRenderGO.SetActive(false);
        currentPoopBubSpawnTime = 0f;
    }

    [Header("PoopBubSpawning")]
    [Range(3, 10)]
    public float poopBubMinSpawnInterval = 3;

    [Range(1, 20)]
    public float poopBubMaxSpawnInterval = 15;

    public GameObject poopBubGO;
    public float spawnPoopBubTelegraphTime = 1f;

    float currentPoopBubSpawnInterval;

    public float animSpeed = .2f;

    public Sprite[] pushFace = new Sprite[2];
    public Sprite defaultFace;

    public SpriteRenderer faceRenderer;
    public GameObject defaultFaceRenderGO;
    public GameObject poopFaceRenderGO;

    int currentSprite = 0;
    float currentAnimateTime = 0f;
    float currentPoopBubSpawnTime = 0f;
    public float poopBubFaceAnimTime = 1.5f;

    bool spawningPoopBub = false;

    public void AnimatePoopFace()
    {
        currentAnimateTime += Time.deltaTime;

        if (currentAnimateTime > animSpeed)
        {
            if (currentSprite == 0)
            {
                currentSprite++;
                faceRenderer.sprite = pushFace[0];
                currentAnimateTime = 0f;
                return;
            }
            else
            {
                currentSprite = 0;
                faceRenderer.sprite = pushFace[1];
                currentAnimateTime = 0f;
                return;
            }
        }
    }

    /// <summary>
    /// Do not hecking input an int value outside of the range of types of bubbles
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="bubbleType"></param>
    public void SpawnBubble(GameObject prefab, int bubbleType)
    {
        float randX = Random.Range(boxCollider2D.bounds.min.x, boxCollider2D.bounds.max.x);
        float randY = Random.Range(boxCollider2D.bounds.min.y, boxCollider2D.bounds.max.y);
        Vector2 randomPosition = new Vector2(randX, randY);

        GameObject newBubbleGO = Instantiate(prefab, randomPosition, Quaternion.identity);
        Bubble newBubble = newBubbleGO.GetComponent<Bubble>();
        newBubble.Initialize(bubbles[bubbleType]);

        newBubble.behaviour = newBubble.type.bubbleBehaviour;
    }
}
