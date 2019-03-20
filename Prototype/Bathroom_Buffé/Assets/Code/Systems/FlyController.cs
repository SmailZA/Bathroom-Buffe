using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class FlyController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D body;
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
        flightInput = Input.GetKey(input.flightInputButton);
        shootInput = Input.GetKeyDown(input.shootInputbutton);

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

        //lookDirection = new Vector2(Input.GetAxis(input.horizontalAxis), Input.GetAxis(input.verticalAxis));

        //rotationValue = Input.GetAxis(input.horizontalAxis);
<<<<<<< HEAD

        Debug.Log("playerIndex: " + playerIndex + " input: " + input);
        //Debug.Log("playerIndex: " + playerIndex + " input: " + input);
=======
>>>>>>> a05643dbafafaab947a3198ce6ec60bd2b52f561
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
    public FloatVariable slowDownVariable;

    public void OnShot(GameObject thisGO, GameObject shotByGO)
    {
        StartCoroutine(ResetSpeed());
    }

    public void OnCollided(GameObject thisGO, GameObject collidedWithGO)
    {
        FlyController controller = collidedWithGO.GetComponent<FlyController>();
        if (controller)
        {
            flightSpeedVariable = slowDownVariable;
            StartCoroutine(ResetSpeed());
        }
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(slowDownDuration);
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
