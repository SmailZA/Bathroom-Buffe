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
    public FloatVariable flightSpeedVariable;

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

        lookDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rotationValue = Input.GetAxis("Horizontal");
    }

    public void SteerFly()
    {
        //         if (rotationValue != 0)
        //         {
        //             body.freezeRotation = false;
        //             float torque = rotationValue * rotationSpeedVariable.value;
        //             body.MoveRotation(body.rotation + -torque);
        //         }
        //         else
        //         {
        //             body.freezeRotation = true;
        //         }

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
