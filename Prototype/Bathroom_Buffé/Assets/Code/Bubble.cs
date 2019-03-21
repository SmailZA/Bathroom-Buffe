using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Bubble : MonoBehaviour
{
    public BubbleType type;
    public BubbleBehaviour behaviour;

    Rigidbody2D body;
    SpriteRenderer spriteRenderer;
    Animator anim;

    bool beginDestroy = false;
    float destroyTime = .5f;
    float destroyTimer = 0f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void Initialize(BubbleType type)
    {
        this.type = type;
        spriteRenderer.sprite = type.sprite;
    }

    public void BeginDestroy()
    {
        anim.SetBool("beginDestroy", true);
        beginDestroy = true;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    private void Update()
    {
        Travel();

        if (!beginDestroy)
            return;

        destroyTimer += Time.deltaTime;

        if (destroyTimer >= destroyTime)
        {
            Destroy(gameObject);
            return;
        }

            AnimatorClipInfo[] animatorClip = anim.GetCurrentAnimatorClipInfo(0);
/*        Debug.Log(animatorClip[0].ToString());*/
        if (animatorClip[0].ToString() == "Pop")
        {

        }
    }

    void Travel()
    {
        /* Cast transform.up to Vector2 because it is a Vector3, Z value is not needed & removed */
        Vector2 newPosition = (Vector2)transform.up * type.speed;
        body.AddForce(newPosition);
        //body.MovePosition(newPosition * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
