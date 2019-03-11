using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class FlyController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D body;

    bool flightInput = false;
    bool shootInput = false;
    float rotationValue;

    public float rotationSpeed = 5f;
    public float flightSpeed = 10f;

    public delegate void OnFireProjectileDelegate(Vector3 location, Quaternion rotation);
    public OnFireProjectileDelegate OnFireProjectile;

    private void Awake() {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
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
    }

    public void SteerFly()
    {
        if (rotationValue != 0)
        {
            body.freezeRotation = false;
            float torque = rotationValue * rotationSpeed;
            body.MoveRotation(body.rotation + -torque);
        }
        else
        {
            body.freezeRotation = true;
        }

        if (flightInput)
        {
            Vector2 forceDirection = transform.up * flightSpeed;
            body.AddForce(forceDirection);
        }

        if (shootInput)
        {
            OnFireProjectile?.Invoke(transform.position, transform.rotation);
        }
    }

    public void AnimateFly()
    {
        anim.SetBool("IsFlying", flightInput);
    }
}
