using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlyController))]
public class Player : MonoBehaviour
{
    [HideInInspector] public FlyController controller;

    PlayerInput input;

    public void Initialize(PlayerInput input)
    {
        this.input = input;
        controller = GetComponent<FlyController>();
    }

    public void Tick()
    {
        controller.SetInputVariables(input);
        controller.SteerFly();
        controller.AnimateFly();
    }
}
