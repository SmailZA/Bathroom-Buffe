using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class FlyController : MonoBehaviour
{
    Animator anim;
    [HideInInspector] public Rigidbody2D body;
    AudioSource audioSource;

    bool flightInput = false;
    bool shootInput = false;
    float rotationValue;
    Vector2 lookDirection;

    public FloatVariable rotationSpeedVariable;
    public FloatVariable defaultFlightSpeedVariable;
    [HideInInspector] public FloatVariable flightSpeedVariable;

    public delegate void OnFireProjectileDelegate(FlyController flyController);
    public OnFireProjectileDelegate OnFireProjectile;

    public delegate void OnIncreaseScoreDelegate(int playerIndex, int amount);
    public OnIncreaseScoreDelegate OnIncreaseScore;

    public delegate void OnDecreaseScoreDelegate(int playerIndex, int amount);
    public OnDecreaseScoreDelegate OnDecreaseScore;

    public delegate void OnHitBubbleDelegate(Vector3 projectilePosition, int bubbleScore);
    public OnHitBubbleDelegate OnHitBubble;

    public int playerIndex;

    bool isPlayingFlySound = false;

    private void Awake() {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Initialize(int index)
    {
        playerIndex = index;
        flightSpeedVariable = defaultFlightSpeedVariable;
    }

    public void SetInputVariables(PlayerInput input)
    {
        flightInput = Input.GetKey(input.flightInputButton) || input.flightInput;
        shootInput = Input.GetKeyDown(input.shootInputbutton) || input.shootInput;

        if (Input.GetKey(input.rotateLeftButton))
        {
            rotationValue = -1f;
        }
        else if (Input.GetKey(input.rotateRightButton))
        {
            rotationValue = 1f;
        }
        else
        {
            rotationValue = 0f;
        }

        if (input.rotateLeft)
        {
            rotationValue = -1f;
        }
        else if (input.rotateRight)
        {
            rotationValue = 1f;
        }
        else
        {
            rotationValue = 0f;
        }

        //lookDirection = new Vector2(Input.GetAxis(input.horizontalAxis), Input.GetAxis(input.verticalAxis));

        //rotationValue = Input.GetAxis(input.horizontalAxis);
    }

    public void SteerFly()
    {
        if (rotationValue != 0)
        {
            body.freezeRotation = false;
            float torque = rotationValue * rotationSpeedVariable.value;
            body.MoveRotation(body.rotation + -torque);
        }
        else
        {
            body.freezeRotation = true;
        }

        if (lookDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        if (flightInput)
        {
            if (!isPlayingFlySound)
            {
                audioSource.Play();
            }
            Vector2 forceDirection = transform.up * flightSpeedVariable.value;
            body.AddForce(forceDirection);
        }
        else
        {
            audioSource.Stop();
        }

        if (shootInput)
        {
            OnFireProjectile?.Invoke(this);
        }
    }

    public void AnimateFly()
    {
        anim.SetBool("IsFlying", flightInput);
    }

    public float slowDownDuration;
    public float speedUpDuration;
    public FloatVariable slowDownVariable;
    public FloatVariable speedUpVariable;

    public void OnShot(GameObject thisGO, GameObject shotByGO)
    {
        body.velocity = Vector3.zero;
        StartCoroutine(ResetSpeed(slowDownDuration));
    }

    public void OnCollided(GameObject thisGO, GameObject collidedWithGO)
    {
        GasBubbleBehaviour poopBubbleBehaviour = collidedWithGO.GetComponent<GasBubbleBehaviour>();
        if (poopBubbleBehaviour)
        {
            flightSpeedVariable = speedUpVariable;
            StartCoroutine(ResetSpeed(speedUpDuration));
            return;
        }

        FlyController controller = collidedWithGO.GetComponent<FlyController>();
        if (controller)
        {
            flightSpeedVariable = slowDownVariable;
            StartCoroutine(ResetSpeed(slowDownDuration));
        }
    }

    IEnumerator ResetSpeed(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        flightSpeedVariable = defaultFlightSpeedVariable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != this)
        {
            Bubble collidedBubble = collision.GetComponent<Bubble>();
            if (collidedBubble)
            {
                BubbleBehaviour collidedBehaviour;
                collidedBehaviour = collidedBubble.type.bubbleBehaviour;
                collidedBehaviour?.OnCollided(collision.gameObject, gameObject);
            }
        }
    }
}
